using Gov.Cscp.Victims.Public.Models;
using Gov.Cscp.Victims.Public.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Serilog;
using System;


namespace Gov.Cscp.Victims.Public.Controllers
{

    [Route("api/[controller]")]
    [Authorize]
    public class DynamicsOrgController : Controller
    {
        private readonly IDynamicsResultService _dynamicsResultService;
        private readonly ILogger _logger;

        public DynamicsOrgController(IDynamicsResultService dynamicsResultService)
        {
            this._dynamicsResultService = dynamicsResultService;
            _logger = Log.Logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrganizationPost model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.Error(new Exception(ModelState.ToString()), $"API call to 'Create' made with invalid model state. Error is:\n{ModelState}. Source = CPU");
                    return BadRequest(ModelState);
                }

                string endpointUrl = "vsd_SetCPUOrgContracts";
                string modelString = System.Text.Json.JsonSerializer.Serialize(model);
                modelString = Helpers.Helpers.updateFortunecookieBindNull(modelString);
                modelString = Helpers.Helpers.removeNullsForStaffUpdate(modelString);
                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, modelString);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unexpected error while creating contact. Source = CPU");
                return BadRequest();
            }
            finally { }
        }


        [HttpPost("SetStaff", Name = "SetStaff")]
        public async Task<IActionResult> SetStaff([FromBody] OrganizationPost model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.Error(new Exception(ModelState.ToString()), $"API call to 'SetStaff' made with invalid model state. Error is:\n{ModelState}. Source = CPU");
                    return BadRequest(ModelState);
                }

                string endpointUrl = "vsd_SetCPUOrgContracts";
                string modelString = System.Text.Json.JsonSerializer.Serialize(model);
                modelString = Helpers.Helpers.updateFortunecookieBindNull(modelString);
                modelString = Helpers.Helpers.removeNullsForStaffUpdate(modelString);
                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, modelString);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unexpected error while setting staff. Source = CPU");
                return BadRequest();
            }
            finally { }
        }
    }
}