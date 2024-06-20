using DotNet8.MiniBankingManagementSystem.Models.Features;
using DotNet8.MiniBankingManagementSystem.Models.Features.Account;

namespace DotNet8.MiniBankingManagementSystem.Modules.Features.Account;

public class BL_Account
{
    #region Initializations

    private readonly DA_Account _dA_Account;

    public BL_Account(DA_Account dA_Account)
    {
        _dA_Account = dA_Account;
    }

    #endregion

    #region GetAccountListAsync

    public async Task<Result<AccountListResponseModel>> GetAccountListAsync()
    {
        return await _dA_Account.GetAccountListAsync();
    }

    #endregion

    #region CreateAccount

    public async Task<Result<AccountResponseModel>> CreateAccount(AccountRequestModel requestModel)
    {
        if (string.IsNullOrEmpty(requestModel.CustomerName))
            throw new Exception("Customer Name cannot be empty.");

        if (requestModel.Balance <= 0)
            throw new Exception("Balance is invalid.");

        if (string.IsNullOrEmpty(requestModel.StateCode))
            throw new Exception("State Code cannot be empty.");

        if (string.IsNullOrEmpty(requestModel.TownshipCode))
            throw new Exception("Township Code cannot be empty.");

        return await _dA_Account.CreateAccount(requestModel);
    }

    #endregion
}
