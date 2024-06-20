using DotNet8.MiniBankingManagementSystem.Models.Features;
using DotNet8.MiniBankingManagementSystem.Models.Features.Withdraw;

namespace DotNet8.MiniBankingManagementSystem.Modules.Features.Withdraw;

public class BL_Withdraw
{
    #region Initialization

    private readonly DA_Withdraw _dA_Withdraw;

    public BL_Withdraw(DA_Withdraw dAWithdraw)
    {
        _dA_Withdraw = dAWithdraw;
    }

    #endregion

    #region Get With Draw List By Account No Async

    public async Task<Result<WithdrawListResponseModel>> GetWithDrawListByAccountNoAsync(
        string accountNo
    )
    {
        if (string.IsNullOrWhiteSpace(accountNo))
            throw new Exception("Account No cannot be empty.");

        return await _dA_Withdraw.GetWithDrawListByAccountNoAsync(accountNo);
    }

    #endregion

    #region CreateWithDrawAsync

    public async Task<Result<WithdrawResponseModel>> CreateWithDrawAsync(
        WithdrawRequestModel requestModel
    )
    {
        if (string.IsNullOrWhiteSpace(requestModel.AccountNo))
            throw new Exception("Account No cannot be empty.");

        if (requestModel.Amount <= 0)
            throw new Exception("Amount cannot be empty.");

        return await _dA_Withdraw.CreateWithDrawAsync(requestModel);
    }

    #endregion
}
