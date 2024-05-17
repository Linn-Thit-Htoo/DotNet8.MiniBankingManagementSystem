using DotNet8.MiniBankingManagementSystem.DbService.Models;
using DotNet8.MiniBankingManagementSystem.Models;
using DotNet8.MiniBankingManagementSystem.Models.Setup.Deposit;
using Microsoft.EntityFrameworkCore;

namespace DotNet8.MiniBankingManagementSystem.Api.Features.Deposit;

public class DA_Deposit
{
    private readonly AppDbContext _appDbContext;

    public DA_Deposit(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    #region GetDepositListByAccountNoAsync

    public async Task<DepositListResponseModel> GetDepositListByAccountNoAsync(string accountNo)
    {
        try
        {
            var depositLst = await _appDbContext.Tbl_Deposit
                .AsNoTracking()
                .Where(x => x.AccountNo == accountNo)
                .OrderByDescending(x => x.DepositId)
                .ToListAsync();

            var lst = depositLst.Select(x => x.Change()).ToList();

            return new DepositListResponseModel
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

    #region CreateDepositAsync

    public async Task<bool> CreateDepositAsync(DepositRequestModel requestModel)
    {
        var transaction = await _appDbContext.Database.BeginTransactionAsync();
        try
        {
            var account = await _appDbContext.Tbl_Account
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.AccountNo == requestModel.AccountNo && x.IsActive)
                ?? throw new Exception("Account Not Found or Inactive.");

            await _appDbContext.Tbl_Deposit.AddAsync(requestModel.Change());
            int result = await _appDbContext.SaveChangesAsync();

            decimal oldBalance = account.Balance;
            decimal newBalance = oldBalance + requestModel.Amount;

            account.Balance = newBalance;
            _appDbContext.Entry(account).State = EntityState.Modified;
            int balanceUpdateResult = await _appDbContext.SaveChangesAsync();

            if (result > 0 && balanceUpdateResult > 0)
            {
                await transaction.CommitAsync();
                return result > 0 && balanceUpdateResult > 0;
            }

            await transaction.RollbackAsync();
            return false;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return false;
        }
    }

    #endregion
}