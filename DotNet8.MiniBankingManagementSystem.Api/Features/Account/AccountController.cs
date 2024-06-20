using DotNet8.MiniBankingManagementSystem.Models.Enums;
using DotNet8.MiniBankingManagementSystem.Models.Features;
using DotNet8.MiniBankingManagementSystem.Models.Features.Account;
using DotNet8.MiniBankingManagementSystem.Models.Resources;
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
        Result<AccountListResponseModel> responseModel;
        try
        {
            responseModel = await _bL_Account.GetAccountListAsync();
            return Ok(responseModel);
        }
        catch (Exception ex)
        {
            return HandleFailure(ex);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] AccountRequestModel requestModel)
    {
        Result<AccountResponseModel> responseModel;
        try
        {
            responseModel = await _bL_Account.CreateAccount(requestModel);
            return Ok(responseModel);
        }
        catch (Exception ex)
        {
            return HandleFailure(ex);
        }
    }
}