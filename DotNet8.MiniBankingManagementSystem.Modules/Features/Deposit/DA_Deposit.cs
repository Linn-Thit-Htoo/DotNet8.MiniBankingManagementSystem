using DotNet8.MiniBankingManagementSystem.DbService.Models;
using DotNet8.MiniBankingManagementSystem.Mapper;
using DotNet8.MiniBankingManagementSystem.Models.Features;
using DotNet8.MiniBankingManagementSystem.Models.Features.Deposit;
using DotNet8.MiniBankingManagementSystem.Models.Resources;
using Microsoft.EntityFrameworkCore;

namespace DotNet8.MiniBankingManagementSystem.Modules.Features.Deposit;

public class DA_Deposit
{
    #region Initializations

    private readonly AppDbContext _appDbContext;

    public DA_Deposit(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    #endregion

    #region GetDepositListByAccountNoAsync

    public async Task<Result<DepositListResponseModel>> GetDepositListByAccountNoAsync(
        string accountNo
    )
    {
        Result<DepositListResponseModel> responseModel;
        try
        {
            var depositLst = await _appDbContext
                .Deposits.AsNoTracking()
                .Where(x => x.AccountNo == accountNo)
                .OrderByDescending(x => x.DepositId)
                .ToListAsync();

            var lst = depositLst.Select(x => x.Change()).ToList();
            var model = new DepositListResponseModel { DataLst = lst };

            responseModel = Result<DepositListResponseModel>.SuccessResult(model);
        }
        catch (Exception ex)
        {
            responseModel = Result<DepositListResponseModel>.FailureResult(ex);
        }

        return responseModel;
    }

    #endregion

    #region CreateDepositAsync

    public async Task<Result<DepositResponseModel>> CreateDepositAsync(
        DepositRequestModel requestModel
    )
    {
        var transaction = await _appDbContext.Database.BeginTransactionAsync();
        Result<DepositResponseModel> responseModel;
        try
        {
            var account = await _appDbContext
                .Accounts.AsNoTracking()
                .FirstOrDefaultAsync(x => x.AccountNo == requestModel.AccountNo && x.IsActive);

            if (account is null)
            {
                responseModel = Result<DepositResponseModel>.FailureResult(
                    MessageResource.NotFound
                );
                goto result;
            }

            await _appDbContext.Deposits.AddAsync(requestModel.Change());

            decimal oldBalance = account.Balance;
            decimal newBalance = oldBalance + requestModel.Amount;

            account.Balance = newBalance;
            _appDbContext.Entry(account).State = EntityState.Modified;
            int result = await _appDbContext.SaveChangesAsync();

            responseModel = Result<DepositResponseModel>.ExecuteResult(result);
            if (responseModel.Success)
            {
                await transaction.CommitAsync();
                goto result;
            }

            await transaction.RollbackAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            responseModel = Result<DepositResponseModel>.FailureResult(ex);
        }

        result:
        return responseModel;
    }

    #endregion
}
