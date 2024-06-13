using DotNet8.MiniBankingManagementSystem.Models.Features.WithDraw;
using DotNet8.MiniBankingManagementSystem.Modules.Features.Withdraw;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8.MiniBankingManagementSystem.Api.Features.WithDraw;

[Route("api/v1/withdraws")]
[ApiController]
public class WithdrawController : ControllerBase
{
    private readonly BL_Withdraw _bL_Withdraw;

    public WithdrawController(BL_Withdraw bLWithdraw)
    {
        _bL_Withdraw = bLWithdraw;
    }

    #region GetWithDrawListByAccountNo

    [HttpGet]
    public async Task<IActionResult> GetWithDrawListByAccountNo(string accountNo)
    {
        try
        {
            return Ok(await _bL_Withdraw.GetWithDrawListByAccountNoAsync(accountNo));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion

    #region CreateWithDraw

    [HttpPost]
    public async Task<IActionResult> CreateWithDraw([FromBody] WithDrawRequestModel requestModel)
    {
        try
        {
            return await _bL_Withdraw.CreateWithDrawAsync(requestModel) ? StatusCode(201, "Successful.") : BadRequest("Fail.");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion
}