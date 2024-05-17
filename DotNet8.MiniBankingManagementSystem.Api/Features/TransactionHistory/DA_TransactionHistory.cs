using DotNet8.MiniBankingManagementSystem.DbService.Models;
using DotNet8.MiniBankingManagementSystem.Models;
using DotNet8.MiniBankingManagementSystem.Models.Setup.TransactionHistory;
using Microsoft.EntityFrameworkCore;

namespace DotNet8.MiniBankingManagementSystem.Api.Features.TransactionHistory;

public class DA_TransactionHistory
{
    #region Initializations

    private readonly AppDbContext _appDbContext;

    public DA_TransactionHistory(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    #endregion

    #region GetTransactionHistoryListByAccountNoAsync

    public async Task<TransactionHistoryListResponseModel> GetTransactionHistoryListByAccountNoAsync(string accountNo)
    {
        try
        {
            var dataLst = await _appDbContext.Tbl_TransactionHistory
                .AsNoTracking()
                .OrderByDescending(x => x.TransactionHistoryId)
                .Where(x => x.FromAccountNo == accountNo)
                .ToListAsync();

            var lst = dataLst.Select(x => x.Change()).ToList();

            return new TransactionHistoryListResponseModel
            {
                DataLst = lst
            };
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion

    #region CreateTransactionAsync

    public async Task<bool> CreateTransactionAsync(TransactionRequestModel requestModel)
    {
        var transaction = await _appDbContext.Database.BeginTransactionAsync();
        try
        {
            #region Check From Account

            var fromAccount = await _appDbContext.Tbl_Account
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.AccountNo == requestModel.FromAccountNo && x.IsActive)
                ?? throw new Exception("From Account Not Found or Inactive.");

            #endregion

            #region Check To Account

            var toAccount = await _appDbContext.Tbl_Account
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.AccountNo == requestModel.ToAccountNo && x.IsActive)
                ?? throw new Exception("To Account Not Found or Inactive.");

            #endregion

            #region Check Balance Insufficient

            decimal fromAccountBalance = fromAccount.Balance;
            if (requestModel.Amount > fromAccountBalance)
            {
                throw new Exception("Insufficient Balance.");
            }

            #endregion

            // transaction history by today date by account no
            #region Fetch Transaction History By today date by account no

            var today = DateTime.Today;
            var transactionHistory = await _appDbContext.Tbl_TransactionHistory
                .AsNoTracking()
                .Where(x => x.TransactionDate == today && x.FromAccountNo == requestModel.FromAccountNo)
                .ToListAsync();

            decimal totalTransactionAmount = 0;
            transactionHistory.ForEach(x => totalTransactionAmount += x.Amount);

            #endregion

            #region Check Account level limitation

            decimal fromAccLevel = fromAccount.AccountLevel;
            long limitedBalance = 0;
            if (fromAccLevel == 1m)
            {
                limitedBalance = 1000000; // 10 lakhs
                if (totalTransactionAmount > limitedBalance)
                {
                    throw new Exception("You exceed the transfer limit for today.");
                }
            }

            if (fromAccLevel == 1.5m)
            {
                limitedBalance = 3000000; // 30 lakhs
                if (totalTransactionAmount > limitedBalance)
                {
                    throw new Exception("You exceed the transfer limit for today.");
                }
            }

            if (fromAccLevel == 2m)
            {
                limitedBalance = 5000000; // 50 lakhs
                if (totalTransactionAmount > limitedBalance)
                {
                    throw new Exception("You exceed the transfer limit for today.");
                }
            }

            #endregion

            #region Reduce Balance From Account

            decimal reducedFromAccountAmount = fromAccount.Balance - requestModel.Amount;
            fromAccount.Balance = reducedFromAccountAmount;
            _appDbContext.Entry(fromAccount).State = EntityState.Modified;
            int fromAccountSavingResult = await _appDbContext.SaveChangesAsync();
            if (fromAccountSavingResult <= 0)
            {
                throw new Exception("Transferring Fail.");
            }

            #endregion

            #region Add Amount To Account

            decimal addedToAccountAmount = toAccount.Balance + requestModel.Amount;
            toAccount.Balance = addedToAccountAmount;
            _appDbContext.Entry(toAccount).State = EntityState.Modified;
            int toAccountSavingResult = await _appDbContext.SaveChangesAsync();
            if (toAccountSavingResult <= 0)
            {
                throw new Exception("Transferring Fail.");
            }

            #endregion

            #region Add Transaction History

            await _appDbContext.Tbl_TransactionHistory.AddAsync(requestModel.Change());
            int transactionHistorySavingResult = await _appDbContext.SaveChangesAsync();
            if (transactionHistorySavingResult <= 0)
            {
                throw new Exception("Transferring Fail.");
            }

            #endregion


            if (fromAccountSavingResult > 0 && toAccountSavingResult > 0 && transactionHistorySavingResult > 0)
            {
                await transaction.CommitAsync();
                return fromAccountSavingResult > 0 && toAccountSavingResult > 0 && transactionHistorySavingResult > 0;
            }

            await transaction.RollbackAsync();
            return false;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception(ex.Message);
        }
    }

    #endregion
}