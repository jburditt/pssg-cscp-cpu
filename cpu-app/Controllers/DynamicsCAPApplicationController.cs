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
    public class DynamicsCAPApplicationController : Controller
    {
        private readonly IDynamicsResultService _dynamicsResultService;
        private readonly ILogger _logger;

        public DynamicsCAPApplicationController(IDynamicsResultService dynamicsResultService)
        {
            this._dynamicsResultService = dynamicsResultService;
            _logger = Log.Logger;
        }

        [HttpGet("{businessBceid}/{userBceid}/{scheduleFId}")]
        public async Task<IActionResult> GetCAPApplication(string businessBceid, string userBceid, string scheduleFId)
        {
            try
            {
                string requestJson = "{\"UserBCeID\":\"" + userBceid + "\",\"BusinessBCeID\":\"" + businessBceid + "\"}";
                string endpointUrl = "vsd_contracts(" + scheduleFId + ")/Microsoft.Dynamics.CRM.vsd_GetCPUScheduleF_CAP";

                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, requestJson);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected error while getting cap application info 'vsd_GetCPUScheduleF_CAP'. Contract id = {scheduleFId}. Source = CPU");
                return BadRequest();
            }
            finally { }
        }

        [HttpPost]
        public async Task<IActionResult> SetCAPApplication([FromBody] CAPApplicationPost model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.Error($"API call to 'SetCAPApplication' made with invalid model state. Error is:\n{ModelState}. Source = CPU");
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
                _logger.Error(e, "Unexpected error while submitting cap application. Source = CPU");
                return BadRequest();
            }
            finally { }
        }
    }
}