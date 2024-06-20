using DotNet8.MiniBankingManagementSystem.Shared;

namespace DotNet8.MiniBankingManagementSystem.Models.Features.TransactionHistory;

public class TransactionRequestModel
{
    public string FromAccountNo { get; set; } = null!;
    public string ToAccountNo { get; set; } = null!;
    public decimal Amount { get; set; }

    public Result<TransactionResponseModel> IsValid()
    {
        Result<TransactionResponseModel> responseModel;

        if (FromAccountNo.IsNullOrEmpty())
        {
            responseModel = Result<TransactionResponseModel>.FailureResult("From Account No cannot be empty.");
            goto result;
        }

        if (ToAccountNo.IsNullOrEmpty())
        {
            responseModel = Result<TransactionResponseModel>.FailureResult("To Account No cannot be empty.");
            goto result;
        }

        if (Amount <= 0)
        {
            responseModel = Result<TransactionResponseModel>.FailureResult("Amount cannot be empty.");
            goto result;
        }

        responseModel = Result<TransactionResponseModel>.SuccessResult();

    result:
        return responseModel;
    }
}
