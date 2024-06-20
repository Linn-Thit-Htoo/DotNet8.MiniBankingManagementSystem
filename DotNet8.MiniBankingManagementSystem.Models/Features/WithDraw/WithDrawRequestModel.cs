using DotNet8.MiniBankingManagementSystem.Shared;

namespace DotNet8.MiniBankingManagementSystem.Models.Features.Withdraw;

public class WithdrawRequestModel
{
    public string AccountNo { get; set; } = null!;
    public decimal Amount { get; set; }

    public Result<WithdrawResponseModel> IsValid()
    {
        Result<WithdrawResponseModel> responseModel;

        if (AccountNo.IsNullOrEmpty())
        {
            responseModel = Result<WithdrawResponseModel>.FailureResult(
                "Account No cannot be empty."
            );
            goto result;
        }

        if (Amount <= 0)
        {
            responseModel = Result<WithdrawResponseModel>.FailureResult("Amount cannot be empty.");
            goto result;
        }

        responseModel = Result<WithdrawResponseModel>.SuccessResult();

        result:
        return responseModel;
    }
}
