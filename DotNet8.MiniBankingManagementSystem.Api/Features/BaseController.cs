using DotNet8.MiniBankingManagementSystem.Models.Enums;
using DotNet8.MiniBankingManagementSystem.Models.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8.MiniBankingManagementSystem.Api.Features
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult Content(object? obj)
        {
            return Ok(JsonConvert.SerializeObject(obj));
        }

        protected IActionResult Accepted(EnumRespType enumRespType)
        {
            switch (enumRespType)
            {
                case EnumRespType.Created:
                    return StatusCode(201, MessageResource.SaveSuccess);
                case EnumRespType.Accepted:
                    return StatusCode(202, MessageResource.SaveSuccess);
                case EnumRespType.Deleted:
                    return StatusCode(202, MessageResource.DeleteSuccess);
                case EnumRespType.None:
                default:
                    return BadRequest("Invalid Response Type.");
            }
        }

        protected IActionResult HandleFailure(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
