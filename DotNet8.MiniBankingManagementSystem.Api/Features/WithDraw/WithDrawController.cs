using DotNet8.MiniBankingManagementSystem.Models.Setup.WithDraw;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8.MiniBankingManagementSystem.Api.Features.WithDraw;

[Route("api/v1/withdraws")]
[ApiController]
public class WithDrawController : ControllerBase
{
    private readonly BL_WithDraw _bL_WithDraw;

    public WithDrawController(BL_WithDraw bL_WithDraw)
    {
        _bL_WithDraw = bL_WithDraw;
    }

    #region GetWithDrawListByAccountNo

    [HttpGet]
    public async Task<IActionResult> GetWithDrawListByAccountNo(string accountNo)
    {
        try
        {
            return Ok(await _bL_WithDraw.GetWithDrawListByAccountNoAsync(accountNo));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion

    [HttpPost]
    public async Task<IActionResult> CreateWithDraw([FromBody] WithDrawRequestModel requestModel)
    {
        try
        {
            return await _bL_WithDraw.CreateWithDrawAsync(requestModel) ? StatusCode(201, "Successful.") : BadRequest("Fail.");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
