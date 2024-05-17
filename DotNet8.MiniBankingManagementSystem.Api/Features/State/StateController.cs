﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8.MiniBankingManagementSystem.Api.Features.State
{
    [Route("api/v1/states")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly BL_State _bL_State;

        public StateController(BL_State bL_State)
        {
            _bL_State = bL_State;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatesList()
        {
            try
            {
                return Ok(await _bL_State.GetStateListAsync());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateStateList()
        {
            try
            {
                int result = await _bL_State.CreateStatesAsync();

                return result > 0 ? StatusCode(201, "State Data Migration Successful.") : BadRequest("Fail.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}