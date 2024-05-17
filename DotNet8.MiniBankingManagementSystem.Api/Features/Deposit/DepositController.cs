using DotNet8.MiniBankingManagementSystem.Models.Setup.Deposit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8.MiniBankingManagementSystem.Api.Features.Deposit;

[Route("api/v1/deposits")]
[ApiController]
public class DepositController : ControllerBase
{
    private readonly BL_Deposit _bL_Deposit;

    public DepositController(BL_Deposit bL_Deposit)
    {
        _bL_Deposit = bL_Deposit;
    }

    #region GetDepositListByAccountNo

    [HttpGet]
    public async Task<IActionResult> GetDepositListByAccountNo(string accountNo)
    {
        try
        {
            return Ok(await _bL_Deposit.GetDepositListByAccountNoAsync(accountNo));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
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
            throw new Exception(ex.Message);
        }
    }

    #endregion
}