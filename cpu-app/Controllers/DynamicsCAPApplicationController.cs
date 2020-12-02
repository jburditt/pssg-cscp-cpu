using Gov.Cscp.Victims.Public.Models;
using Gov.Cscp.Victims.Public.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace Gov.Cscp.Victims.Public.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class DynamicsCAPApplicationController : Controller
    {
        private readonly IDynamicsResultService _dynamicsResultService;

        public DynamicsCAPApplicationController(IDynamicsResultService dynamicsResultService)
        {
            this._dynamicsResultService = dynamicsResultService;
        }

        [HttpGet("{businessBceid}/{userBceid}/{scheduleFId}")]
        public async Task<IActionResult> GetCAPApplication(string businessBceid, string userBceid, string scheduleFId)
        {
            try
            {
                // convert the parameters to a json string
                string requestJson = "{\"UserBCeID\":\"" + userBceid + "\",\"BusinessBCeID\":\"" + businessBceid + "\"}";
                // set the endpoint action
                string endpointUrl = "vsd_contracts(" + scheduleFId + ")/Microsoft.Dynamics.CRM.vsd_GetCPUScheduleF_CAP";

                // get the response
                DynamicsResult result = await _dynamicsResultService.Post(endpointUrl, requestJson);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            finally { }
        }

        [HttpPost]
        public async Task<IActionResult> SetCAPApplication([FromBody] CAPApplicationPost model)
        {
            try
            {
                if (model == null)
                {
                    return StatusCode(502);
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