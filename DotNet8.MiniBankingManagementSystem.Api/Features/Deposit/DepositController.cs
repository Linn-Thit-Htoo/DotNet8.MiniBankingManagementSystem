using DotNet8.MiniBankingManagementSystem.Models.Features.Deposit;
using DotNet8.MiniBankingManagementSystem.Modules.Features.Deposit;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8.MiniBankingManagementSystem.Api.Features.Deposit;

[Route("api/v1/deposits")]
[ApiController]
public class DepositController : BaseController
{
    #region Initializations

    private readonly BL_Deposit _bL_Deposit;

    public DepositController(BL_Deposit bL_Deposit)
    {
        _bL_Deposit = bL_Deposit;
    }

    #endregion

    #region GetDepositListByAccountNo

    [HttpGet]
    public async Task<IActionResult> GetDepositListByAccountNo(string accountNo)
    {
        try
        {
            return Content(await _bL_Deposit.GetDepositListByAccountNoAsync(accountNo));
        }
        catch (Exception ex)
        {
            return HandleFailure(ex);
        }
    }

    #endregion

    #region CreateDeposit

    [HttpPost]
    public async Task<IActionResult> CreateDeposit([FromBody] DepositRequestModel requestModel)
    {
        try
        {
            bool isSuccess = await _bL_Deposit.CreateDepositAsync(requestModel);
            return isSuccess ? StatusCode(201, "Successful.") : BadRequest("Fail.");
        }
        catch (Exception ex)
        {
            return HandleFailure(ex);
        }
    }

    #endregion
}