using DotNet8.MiniBankingManagementSystem.Models.Features.Account;
using DotNet8.MiniBankingManagementSystem.Modules.Features.Account;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8.MiniBankingManagementSystem.Api.Features.Account;

[Route("api/v1/accounts")]
[ApiController]
public class AccountController : BaseController
{
    private readonly BL_Account _bL_Account;

    public AccountController(BL_Account bL_Account)
    {
        _bL_Account = bL_Account;
    }

    [HttpGet]
    public async Task<IActionResult> GetAccountList()
    {
        try
        {
            var responseModel = await _bL_Account.GetAccountListAsync();
            return Content(responseModel);
        }
        catch (Exception ex)
        {
            return HandleFailure(ex);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] AccountRequestModel requestModel)
    {
        try
        {
            var result = requestModel.IsValid();
            if (!result.Success)
                return BadRequest(result);

            var responseModel = await _bL_Account.CreateAccount(requestModel);
            return Content(responseModel);
        }
        catch (Exception ex)
        {
            return HandleFailure(ex);
        }
    }
}
