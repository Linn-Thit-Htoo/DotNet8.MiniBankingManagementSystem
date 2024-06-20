using DotNet8.MiniBankingManagementSystem.Models.Features;
using DotNet8.MiniBankingManagementSystem.Models.Features.Deposit;

namespace DotNet8.MiniBankingManagementSystem.Modules.Features.Deposit;

public class BL_Deposit
{
    #region Initializations

    private readonly DA_Deposit _dA_Deposit;

    public BL_Deposit(DA_Deposit dA_Deposit)
    {
        _dA_Deposit = dA_Deposit;
    }

    #endregion

    #region GetDepositListByAccountNoAsync

    public async Task<Result<DepositListResponseModel>> GetDepositListByAccountNoAsync(string accountNo)
    {
        if (string.IsNullOrWhiteSpace(accountNo))
            throw new Exception("Account No cannot be empty.");

        return await _dA_Deposit.GetDepositListByAccountNoAsync(accountNo);
    }

    #endregion

    #region CreateDepositAsync

    public async Task<Result<DepositResponseModel>> CreateDepositAsync(DepositRequestModel requestModel)
    {
        if (string.IsNullOrWhiteSpace(requestModel.AccountNo))
            throw new Exception("Account No cannot be empty.");

        if (requestModel.Amount <= 0)
            throw new Exception("Amount is invalid.");

        return await _dA_Deposit.CreateDepositAsync(requestModel);
    }

    #endregion
}