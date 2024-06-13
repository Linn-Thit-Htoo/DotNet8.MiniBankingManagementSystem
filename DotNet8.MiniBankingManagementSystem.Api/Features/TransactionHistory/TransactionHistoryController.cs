using DotNet8.MiniBankingManagementSystem.Models.Features.TransactionHistory;
using DotNet8.MiniBankingManagementSystem.Modules.Features.TransactionHistory;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8.MiniBankingManagementSystem.Api.Features.TransactionHistory;

[Route("api/v1/transaction-histories")]
[ApiController]
public class TransactionHistoryController : ControllerBase
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
            return Ok(await _bL_TransactionHistory.GetTransactionHistoryListByAccountNoAsync(accountNo));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion

    #region CreateTransaction

    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] TransactionRequestModel requestModel)
    {
        try
        {
            return await _bL_TransactionHistory.CreateTransactionAsync(requestModel) ? StatusCode(201, "Successful.") : BadRequest("Fail.");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion
}