using Microsoft.AspNetCore.Mvc;

namespace DotNet8.MiniBankingManagementSystem.Api.Features.Township
{
    [Route("api/v1/townships")]
    [ApiController]
    public class TownshipController : ControllerBase
    {
        private readonly BL_Township _bL_Township;

        public TownshipController(BL_Township bL_Township)
        {
            _bL_Township = bL_Township;
        }

        [HttpGet]
        public async Task<IActionResult> GetTownshipList()
        {
            try
            {
                return Ok(await _bL_Township.GetTownshipListAsync());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTownshipList()
        {
            try
            {
                int result = await _bL_Township.CreateTownshipListAsync();

                return result > 0 ? StatusCode(201, "Township Data Migration Successful.") : BadRequest("Fail.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}