using DotNet8.MiniBankingManagementSystem.Models.Setup.WithDraw;

namespace DotNet8.MiniBankingManagementSystem.Api.Features.WithDraw;

public class BL_Withdraw
{
    #region Initialization

    private readonly DA_Withdraw _dA_Withdraw;

    public BL_Withdraw(DA_Withdraw dAWithdraw)
    {
        _dA_Withdraw = dAWithdraw;
    }

    #endregion

    #region GetWithDrawListByAccountNoAsync

    public async Task<WithDrawListResponseModel> GetWithDrawListByAccountNoAsync(string accountNo)
    {
        if (string.IsNullOrWhiteSpace(accountNo))
            throw new Exception("Account No cannot be empty.");

        return await _dA_Withdraw.GetWithDrawListByAccountNoAsync(accountNo);
    }

    #endregion

    #region CreateWithDrawAsync

    public async Task<bool> CreateWithDrawAsync(WithDrawRequestModel requestModel)
    {
        if (string.IsNullOrWhiteSpace(requestModel.AccountNo))
            throw new Exception("Account No cannot be empty.");

        if (requestModel.Amount <= 0)
            throw new Exception("Amount cannot be empty.");

        return await _dA_Withdraw.CreateWithDrawAsync(requestModel);
    }

    #endregion
}