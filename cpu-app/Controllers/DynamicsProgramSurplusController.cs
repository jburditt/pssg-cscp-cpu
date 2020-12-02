using Gov.Cscp.Victims.Public.Models;
using Gov.Cscp.Victims.Public.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class DynamicsProgramSurplusController : Controller
    {
        private readonly IDynamicsResultService _dynamicsResultService;

        public DynamicsProgramSurplusController(IDynamicsResultService dynamicsResultService)
        {
            this._dynamicsResultService = dynamicsResultService;
        }

        [HttpGet("{businessBceid}/{userBceid}/{surplusId}")]
        public async Task<IActionResult> GetProgramSurplus(string businessBceid, string userBceid, string surplusId)
        {
            try
            {
                // convert the parameters to a json string
                string requestJson = "{\"UserBCeID\":\"" + userBceid + "\",\"BusinessBCeID\":\"" + businessBceid + "\"}";
                // set the endpoint action
                string endpointUrl = "vsd_surplusplanreports(" + surplusId + ")/Microsoft.Dynamics.CRM.vsd_GetCPUSurplusPlan";

                // get the response
                DynamicsResult result = await _dynamicsResultService.Post(endpointUrl, requestJson);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            finally { }
        }

        [HttpPost]
        public async Task<IActionResult> SetProgramSurplus([FromBody] ProgramSurplusPost model)
        {
            try
            {
                if (model == null)
                {
                    return StatusCode(502);
                }

                string endpointUrl = "vsd_SetCPUOrgContracts";
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.IgnoreNullValues = true;
                // turn the model into a string
                string modelString = System.Text.Json.JsonSerializer.Serialize(model, options);
                modelString = Helpers.Helpers.updateFortunecookieBindNull(modelString);
                DynamicsResult result = await _dynamicsResultService.Post(endpointUrl, modelString);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            finally { }
        }
    }
}