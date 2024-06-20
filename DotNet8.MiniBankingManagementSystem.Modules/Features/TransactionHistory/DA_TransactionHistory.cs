using Dapper;
using DotNet8.MiniBankingManagementSystem.DbService.Models;
using DotNet8.MiniBankingManagementSystem.Models.Features.TransactionHistory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using DotNet8.MiniBankingManagementSystem.Mapper;
using DotNet8.MiniBankingManagementSystem.Models.Features;
using DotNet8.MiniBankingManagementSystem.Models.Resources;

namespace DotNet8.MiniBankingManagementSystem.Modules.Features.TransactionHistory;

public class DA_TransactionHistory
{
    #region Initializations

    private readonly AppDbContext _appDbContext;
    private readonly IConfiguration _configuration;

    public DA_TransactionHistory(AppDbContext appDbContext, IConfiguration configuration)
    {
        _appDbContext = appDbContext;
        _configuration = configuration;
    }

    #endregion

    #region GetTransactionHistoryListByAccountNoAsync

    public async Task<Result<TransactionHistoryListResponseModel>> GetTransactionHistoryListByAccountNoAsync(string accountNo)
    {
        Result<TransactionHistoryListResponseModel> responseModel;
        try
        {
            using IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DbConnection"));
            var parameters = new
            {
                FromAccountNo = accountNo
            };

            IEnumerable<TransactionDataModel> dataLst = await db
                .QueryAsync<TransactionDataModel>(
                "Sp_GetTransactionHistoryListByAccountNo",
                parameters,
                commandType: CommandType.StoredProcedure);

            var lst = dataLst.Select(x => new TransactionDataModel()
            {
                Amount = Math.Round(x.Amount),
                ReceiverName = x.ReceiverName,
                SenderName = x.SenderName,
                TransactionDate = x.TransactionDate,
                TransactionHistoryId = x.TransactionHistoryId
            }).ToList();

            var model = new TransactionHistoryListResponseModel
            {
                DataLst = lst
            };


            responseModel = Result<TransactionHistoryListResponseModel>.SuccessResult(model);
        }
        catch (Exception ex)
        {
            responseModel = Result<TransactionHistoryListResponseModel>.FailureResult(ex);
        }

        return responseModel;
    }

    #endregion

    #region CreateTransactionAsync

    public async Task<Result<TransactionResponseModel>> CreateTransactionAsync(TransactionRequestModel requestModel)
    {
        var transaction = await _appDbContext.Database.BeginTransactionAsync();
        Result<TransactionResponseModel> responseModel;
        try
        {
            #region Check From Account

            var fromAccount = await _appDbContext.Accounts
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.AccountNo == requestModel.FromAccountNo && x.IsActive);

            if (fromAccount is null)
            {
                responseModel = Result<TransactionResponseModel>.FailureResult(MessageResource.NotFound);
                goto result;
            }

            #endregion

            #region Check To Account

            var toAccount = await _appDbContext.Accounts
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.AccountNo == requestModel.ToAccountNo && x.IsActive);

            if (toAccount is null)
            {
                responseModel = Result<TransactionResponseModel>.FailureResult(MessageResource.NotFound);
                goto result;
            }

            #endregion

            #region Check Balance Insufficient

            decimal fromAccountBalance = fromAccount.Balance;

            if (requestModel.Amount > fromAccountBalance)
            {
                responseModel = Result<TransactionResponseModel>.FailureResult("Insufficient Balance.");
                goto result;
            }

            #endregion

            #region Fetch Transaction History By today date by account no

            var today = DateTime.Today;
            var transactionHistory = await _appDbContext.TransactionHistories
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
                    responseModel = Result<TransactionResponseModel>.FailureResult("You exceed the transfer limit for today.");
                    goto result;
                }
            }

            if (fromAccLevel == 1.5m)
            {
                limitedBalance = 3000000; // 30 lakhs
                if (totalTransactionAmount > limitedBalance)
                {
                    responseModel = Result<TransactionResponseModel>.FailureResult("You exceed the transfer limit for today.");
                    goto result;
                }
            }

            if (fromAccLevel == 2m)
            {
                limitedBalance = 5000000; // 50 lakhs
                if (totalTransactionAmount > limitedBalance)
                {
                    responseModel = Result<TransactionResponseModel>.FailureResult("You exceed the transfer limit for today.");
                    goto result;
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
                responseModel = Result<TransactionResponseModel>.FailureResult("Transferring Fail.");
                goto result;
            }

            #endregion

            #region Add Amount To Account

            decimal addedToAccountAmount = toAccount.Balance + requestModel.Amount;
            toAccount.Balance = addedToAccountAmount;
            _appDbContext.Entry(toAccount).State = EntityState.Modified;
            int toAccountSavingResult = await _appDbContext.SaveChangesAsync();
            if (toAccountSavingResult <= 0)
            {
                responseModel = Result<TransactionResponseModel>.FailureResult("Transferring Fail.");
                goto result;
            }

            #endregion

            #region Add Transaction History

            await _appDbContext.TransactionHistories.AddAsync(requestModel.Change());
            int transactionHistorySavingResult = await _appDbContext.SaveChangesAsync();
            if (transactionHistorySavingResult <= 0)
            {
                responseModel = Result<TransactionResponseModel>.FailureResult("Transferring Fail.");
                goto result;
            }

            #endregion


            if (fromAccountSavingResult > 0 && toAccountSavingResult > 0 && transactionHistorySavingResult > 0)
            {
                await transaction.CommitAsync();
                responseModel = Result<TransactionResponseModel>.SuccessResult(MessageResource.SaveSuccess);
                goto result;
            }

            await transaction.RollbackAsync();
            responseModel = Result<TransactionResponseModel>.FailureResult(MessageResource.SaveFail);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            responseModel = Result<TransactionResponseModel>.FailureResult(ex);
        }

    result:
        return responseModel;
    }

    #endregion
}