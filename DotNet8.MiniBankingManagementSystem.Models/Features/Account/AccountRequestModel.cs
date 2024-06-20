using DotNet8.MiniBankingManagementSystem.Shared;

namespace DotNet8.MiniBankingManagementSystem.Models.Features.Account;

public class AccountRequestModel
{
    public string CustomerName { get; set; } = null!;
    public decimal AccountLevel { get; set; }
    public decimal Balance { get; set; }
    public string? CustomerCode { get; set; }
    public string StateCode { get; set; } = null!;
    public string TownshipCode { get; set; } = null!;

    public Result<AccountResponseModel> IsValid()
    {
        Result<AccountResponseModel> responseModel;

        if (CustomerName.IsNullOrEmpty())
        {
            responseModel = Result<AccountResponseModel>.FailureResult("Customer Name cannot be empty.");
            goto result;
        }

        if (Balance <= 0)
        {
            responseModel = Result<AccountResponseModel>.FailureResult("Balance is invalid.");
            goto result;
        }

        if (StateCode.IsNullOrEmpty())
        {
            responseModel = Result<AccountResponseModel>.FailureResult("State Code cannot be empty.");
            goto result;
        }

        if (TownshipCode.IsNullOrEmpty())
        {
            responseModel = Result<AccountResponseModel>.FailureResult("Township Code cannot be empty.");
            goto result;
        }

        responseModel = Result<AccountResponseModel>.SuccessResult();

    result:
        return responseModel;
    }
}
