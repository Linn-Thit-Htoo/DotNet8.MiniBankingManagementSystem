using DotNet8.MiniBankingManagementSystem.DbService.Models;
using DotNet8.MiniBankingManagementSystem.Models;
using DotNet8.MiniBankingManagementSystem.Models.Setup.WithDraw;
using Microsoft.EntityFrameworkCore;

namespace DotNet8.MiniBankingManagementSystem.Api.Features.WithDraw;

public class DA_WithDraw
{
    #region Initializations

    private AppDbContext _appDbContext;

    public DA_WithDraw(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    #endregion

    #region GetWithDrawListByAccountNoAsync

    public async Task<WithDrawListResponseModel> GetWithDrawListByAccountNoAsync(string accountNo)
    {
        try
        {
            var dataLst = await _appDbContext.Tbl_WithDraw
                .AsNoTracking()
                .Where(x => x.AccountNo == accountNo)
                .OrderByDescending(x => x.WithDrawId)
                .ToListAsync();

            var lst = dataLst.Select(x => x.Change()).ToList();

            return new WithDrawListResponseModel
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

    #region CreateWithDrawAsync

    public async Task<bool> CreateWithDrawAsync(WithDrawRequestModel requestModel)
    {
        var transaction = await _appDbContext.Database.BeginTransactionAsync();
        try
        {
            var account = await _appDbContext.Tbl_Account
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.AccountNo == requestModel.AccountNo && x.IsActive)
                ?? throw new Exception("Account Not Found or Inactive.");

            decimal oldBalance = account.Balance;
            if (requestModel.Amount > oldBalance)
            {
                throw new Exception("Your balance is insufficient.");
            }

            decimal newBalance = oldBalance - requestModel.Amount;
            account.Balance = newBalance;
            _appDbContext.Entry(account).State = EntityState.Modified;
            int accountUpdateResult = await _appDbContext.SaveChangesAsync();

            await _appDbContext.Tbl_WithDraw.AddAsync(requestModel.Change());
            int result = await _appDbContext.SaveChangesAsync();

            if (accountUpdateResult > 0 && result > 0)
            {
                await transaction.CommitAsync();
                return accountUpdateResult > 0 && result > 0;
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