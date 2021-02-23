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
    public class DynamicsProgramApplicationController : Controller
    {
        private readonly IDynamicsResultService _dynamicsResultService;
        private readonly ILogger _logger;

        public DynamicsProgramApplicationController(IDynamicsResultService dynamicsResultService)
        {
            this._dynamicsResultService = dynamicsResultService;
            _logger = Log.Logger;
        }

        [HttpGet("{businessBceid}/{userBceid}/{scheduleFId}")]
        public async Task<IActionResult> GetProgramApplication(string businessBceid, string userBceid, string scheduleFId)
        {
            try
            {
                string requestJson = "{\"UserBCeID\":\"" + userBceid + "\",\"BusinessBCeID\":\"" + businessBceid + "\"}";
                string endpointUrl = "vsd_contracts(" + scheduleFId + ")/Microsoft.Dynamics.CRM.vsd_GetCPUScheduleF";

                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, requestJson);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected error while getting program application info 'vsd_GetCPUScheduleF'. Contract id = {scheduleFId}. Source = CPU");
                return BadRequest();
            }
            finally { }
        }

        [HttpPost]
        public async Task<IActionResult> SetProgramApplication([FromBody] ProgramApplicationPost model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.Error($"API call to 'SetProgramApplication' made with invalid model state. Error is:\n{ModelState}. Source = CPU");
                    return BadRequest(ModelState);
                }

                string endpointUrl = "vsd_SetCPUOrgContracts";
                string modelString = System.Text.Json.JsonSerializer.Serialize(model);
                modelString = Helpers.Helpers.updateFortunecookieBindNull(modelString);
                modelString = Helpers.Helpers.removeNullsForProgramApplication(modelString);
                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, modelString);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unexpected error while submitting program application. Source = CPU");
                return BadRequest();
            }
            finally { }
        }
    }
}