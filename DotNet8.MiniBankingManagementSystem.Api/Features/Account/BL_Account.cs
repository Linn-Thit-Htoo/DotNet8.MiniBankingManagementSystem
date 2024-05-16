using DotNet8.MiniBankingManagementSystem.Models.Setup.Account;

namespace DotNet8.MiniBankingManagementSystem.Api.Features.Account;

public class BL_Account
{
    private readonly DA_Account _dA_Account;

    public BL_Account(DA_Account dA_Account)
    {
        _dA_Account = dA_Account;
    }

    public async Task<AccountListResponseModel> GetAccountListAsync()
    {
        return await _dA_Account.GetAccountListAsync();
    }

    public async Task<int> CreateAccount(AccountRequestModel requestModel)
    {
        if (string.IsNullOrEmpty(requestModel.CustomerName))
            throw new Exception("Customer Name cannot be empty.");

        if (requestModel.Balance <= 0)
            throw new Exception("Balance is invalid.");

        if (string.IsNullOrEmpty(requestModel.StateCode))
            throw new Exception("State Code cannot be empty.");

        if (string.IsNullOrEmpty(requestModel.TownshipCode))
            throw new Exception("Township Code cannot be empty.");

        // validate state

        int result = await _dA_Account.CreateAccount(requestModel);

        return result;
    }
}