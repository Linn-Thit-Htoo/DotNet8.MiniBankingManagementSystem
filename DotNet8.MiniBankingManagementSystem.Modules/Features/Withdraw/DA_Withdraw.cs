using DotNet8.MiniBankingManagementSystem.DbService.Models;
using DotNet8.MiniBankingManagementSystem.Mapper;
using DotNet8.MiniBankingManagementSystem.Models.Features;
using DotNet8.MiniBankingManagementSystem.Models.Features.Withdraw;
using DotNet8.MiniBankingManagementSystem.Models.Resources;
using Microsoft.EntityFrameworkCore;

namespace DotNet8.MiniBankingManagementSystem.Modules.Features.Withdraw;

public class DA_Withdraw
{
    #region Initializations

    private AppDbContext _appDbContext;

    public DA_Withdraw(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    #endregion

    #region Get With draw List By Account No Async

    public async Task<Result<WithdrawListResponseModel>> GetWithDrawListByAccountNoAsync(
        string accountNo
    )
    {
        Result<WithdrawListResponseModel> responseModel;
        try
        {
            var dataLst = await _appDbContext
                .Withdraws.AsNoTracking()
                .Where(x => x.AccountNo == accountNo)
                .OrderByDescending(x => x.WithDrawId)
                .ToListAsync();

            var lst = dataLst.Select(x => x.Change()).ToList();
            var model = new WithdrawListResponseModel { DataLst = lst };

            responseModel = Result<WithdrawListResponseModel>.SuccessResult(model);
        }
        catch (Exception ex)
        {
            responseModel = Result<WithdrawListResponseModel>.FailureResult(ex);
        }

        return responseModel;
    }

    #endregion

    #region Create With Draw Async

    public async Task<Result<WithdrawResponseModel>> CreateWithDrawAsync(
        WithdrawRequestModel requestModel
    )
    {
        var transaction = await _appDbContext.Database.BeginTransactionAsync();
        Result<WithdrawResponseModel> responseModel;
        try
        {
            var account =
                await _appDbContext
                    .Accounts.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.AccountNo == requestModel.AccountNo && x.IsActive)
                ?? throw new Exception("Account Not Found or Inactive.");

            decimal oldBalance = account.Balance;
            if (requestModel.Amount > oldBalance)
            {
                responseModel = Result<WithdrawResponseModel>.SuccessResult(
                    "Your balance is insufficient."
                );
                goto result;
            }

            decimal newBalance = oldBalance - requestModel.Amount;
            account.Balance = newBalance;
            _appDbContext.Entry(account).State = EntityState.Modified;
            int accountUpdateResult = await _appDbContext.SaveChangesAsync();

            await _appDbContext.Withdraws.AddAsync(requestModel.Change());
            int result = await _appDbContext.SaveChangesAsync();

            if (accountUpdateResult > 0 && result > 0)
            {
                await transaction.CommitAsync();
                responseModel = Result<WithdrawResponseModel>.SuccessResult(
                    MessageResource.SaveSuccess
                );
                goto result;
            }

            await transaction.RollbackAsync();
            responseModel = Result<WithdrawResponseModel>.FailureResult(MessageResource.SaveFail);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            responseModel = Result<WithdrawResponseModel>.FailureResult(ex);
        }

        result:
        return responseModel;
    }

    #endregion
}
