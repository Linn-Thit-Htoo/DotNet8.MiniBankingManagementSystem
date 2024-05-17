using DotNet8.MiniBankingManagementSystem.Models.Setup.Account;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8.MiniBankingManagementSystem.Api.Features.Account;

[Route("api/v1/accounts")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly BL_Account _bL_Account;

    public AccountController(BL_Account bL_Account)
    {
        _bL_Account = bL_Account;
    }

    [HttpGet]
    public async Task<IActionResult> GetAccountList()
    {
        return Ok(await _bL_Account.GetAccountListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] AccountRequestModel requestModel)
    {
        try
        {
            int result = await _bL_Account.CreateAccount(requestModel);

            return result > 0 ? StatusCode(201, "Account Created.") : BadRequest("Creating Fail.");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}