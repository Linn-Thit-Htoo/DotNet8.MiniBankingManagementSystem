namespace DotNet8.MiniBankingManagementSystem.Api.Features.State;

[Route("api/v1/states")]
[ApiController]
public class StateController : BaseController
{
    #region Initializations

    private readonly BL_State _bL_State;

    public StateController(BL_State bL_State)
    {
        _bL_State = bL_State;
    }

    #endregion

    #region GetStatesList

    [HttpGet]
    public async Task<IActionResult> GetStatesList()
    {
        try
        {
            return Content(await _bL_State.GetStateListAsync());
        }
        catch (Exception ex)
        {
            return HandleFailure(ex);
        }
    }

    #endregion

    #region CreateStateList

    [HttpPost]
    public async Task<IActionResult> CreateStateList()
    {
        try
        {
            var responseModel = await _bL_State.CreateStatesAsync();
            return Content(responseModel);
        }
        catch (Exception ex)
        {
            return HandleFailure(ex);
        }
    }

    #endregion
}
