using DotNet8.MiniBankingManagementSystem.Shared;

namespace DotNet8.MiniBankingManagementSystem.Models.Features.Deposit;

public class DepositRequestModel
{
    public string AccountNo { get; set; } = null!;
    public decimal Amount { get; set; }

    public Result<DepositResponseModel> IsValid()
    {
        Result<DepositResponseModel> responseModel;

        if (AccountNo.IsNullOrEmpty())
        {
            responseModel = Result<DepositResponseModel>.FailureResult("Account No cannot be empty.");
            goto result;
        }

        if (Amount <= 0)
        {
            responseModel = Result<DepositResponseModel>.FailureResult("Amount is invalid.");
            goto result;
        }

        responseModel = Result<DepositResponseModel>.SuccessResult();

    result:
        return responseModel;
    }
}
