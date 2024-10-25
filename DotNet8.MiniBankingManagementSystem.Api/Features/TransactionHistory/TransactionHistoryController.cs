namespace DotNet8.MiniBankingManagementSystem.Api.Features.TransactionHistory;

[Route("api/v1/transaction-histories")]
[ApiController]
public class TransactionHistoryController : BaseController
{
    #region Initializations

    private readonly BL_TransactionHistory _bL_TransactionHistory;

    public TransactionHistoryController(BL_TransactionHistory bL_TransactionHistory)
    {
        _bL_TransactionHistory = bL_TransactionHistory;
    }

    #endregion

    #region GetTransactionListByAccountNo

    [HttpGet]
    public async Task<IActionResult> GetTransactionListByAccountNoAsync(string accountNo)
    {
        try
        {
            return Content(
                await _bL_TransactionHistory.GetTransactionHistoryListByAccountNoAsync(accountNo)
            );
        }
        catch (Exception ex)
        {
            return HandleFailure(ex);
        }
    }

    #endregion

    #region CreateTransaction

    [HttpPost]
    public async Task<IActionResult> CreateTransaction(
        [FromBody] TransactionRequestModel requestModel
    )
    {
        try
        {
            var result = requestModel.IsValid();
            if (!result.Success)
                return BadRequest(result);

            var responseModel = await _bL_TransactionHistory.CreateTransactionAsync(requestModel);
            return Content(responseModel);
        }
        catch (Exception ex)
        {
            return HandleFailure(ex);
        }
    }

    #endregion
}
