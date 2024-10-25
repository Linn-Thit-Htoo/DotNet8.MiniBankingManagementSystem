namespace DotNet8.MiniBankingManagementSystem.Api.Features.WithDraw;

[Route("api/v1/withdraws")]
[ApiController]
public class WithdrawController : BaseController
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
            return Content(await _bL_Withdraw.GetWithDrawListByAccountNoAsync(accountNo));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion

    #region CreateWithDraw

    [HttpPost]
    public async Task<IActionResult> CreateWithDraw([FromBody] WithdrawRequestModel requestModel)
    {
        try
        {
            var result = requestModel.IsValid();
            if (!result.Success)
                return BadRequest(result);

            var responseModel = await _bL_Withdraw.CreateWithDrawAsync(requestModel);
            return Content(responseModel);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion
}
