using Gov.Cscp.Victims.Public.Models;
using Gov.Cscp.Victims.Public.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Serilog;


namespace Gov.Cscp.Victims.Public.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class DynamicsProgramApplicationController : Controller
    {
        private readonly IDynamicsResultService _dynamicsResultService;
        // private readonly Serilog.ILogger _logger;

        public DynamicsProgramApplicationController(IDynamicsResultService dynamicsResultService)
        {
            this._dynamicsResultService = dynamicsResultService;
            // _logger = Log.Logger;
            // _logger.Error(e, "Unexpected error creating annual volume");
        }

        [HttpGet("{businessBceid}/{userBceid}/{scheduleFId}")]
        public async Task<IActionResult> GetProgramApplication(string businessBceid, string userBceid, string scheduleFId)
        {
            try
            {
                // convert the parameters to a json string
                string requestJson = "{\"UserBCeID\":\"" + userBceid + "\",\"BusinessBCeID\":\"" + businessBceid + "\"}";
                // set the endpoint action
                string endpointUrl = "vsd_contracts(" + scheduleFId + ")/Microsoft.Dynamics.CRM.vsd_GetCPUScheduleF";

                // get the response
                DynamicsResult result = await _dynamicsResultService.Post(endpointUrl, requestJson);

                return StatusCode((int)result.statusCode, result.result.ToString());
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
                    return BadRequest(ModelState);
                }

                string endpointUrl = "vsd_SetCPUOrgContracts";
                // turn the model into a string
                string modelString = System.Text.Json.JsonSerializer.Serialize(model);
                modelString = Helpers.Helpers.updateFortunecookieBindNull(modelString);
                modelString = Helpers.Helpers.removeNullsForProgramApplication(modelString);
                //_ownerid_value on the Organization is already ignored by the CRM API, so don't need to remove it
                DynamicsResult result = await _dynamicsResultService.Post(endpointUrl, modelString);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            finally { }
        }
    }
}