using DotNet8.MiniBankingManagementSystem.Models.Setup.Deposit;

namespace DotNet8.MiniBankingManagementSystem.Api.Features.Deposit;

public class BL_Deposit
{
    private readonly DA_Deposit _dA_Deposit;

    public BL_Deposit(DA_Deposit dA_Deposit)
    {
        _dA_Deposit = dA_Deposit;
    }

    #region GetDepositListByAccountNoAsync

    public async Task<DepositListResponseModel> GetDepositListByAccountNoAsync(string accountNo)
    {
        if (string.IsNullOrWhiteSpace(accountNo))
            throw new Exception("Account No cannot be empty.");

        return await _dA_Deposit.GetDepositListByAccountNoAsync(accountNo);
    }

    #endregion

    #region MyRegion

    #endregion
    public async Task<bool> CreateDepositAsync(DepositRequestModel requestModel)
    {
        if (string.IsNullOrWhiteSpace(requestModel.AccountNo))
            throw new Exception("Account No cannot be empty.");

        if (requestModel.Amount <= 0)
            throw new Exception("Amount is invalid.");

        bool isSuccess = await _dA_Deposit.CreateDepositAsync(requestModel);
        return isSuccess;
    }
}