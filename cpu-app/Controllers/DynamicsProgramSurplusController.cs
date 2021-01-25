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
                string requestJson = "{\"UserBCeID\":\"" + userBceid + "\",\"BusinessBCeID\":\"" + businessBceid + "\"}";
                string endpointUrl = "vsd_surplusplanreports(" + surplusId + ")/Microsoft.Dynamics.CRM.vsd_GetCPUSurplusPlan";

                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, requestJson);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            finally { }
        }

        [HttpPost]
        public async Task<IActionResult> SetProgramSurplus([FromBody] ProgramSurplusPost model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string endpointUrl = "vsd_SetCPUOrgContracts";
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.IgnoreNullValues = true;
                string modelString = System.Text.Json.JsonSerializer.Serialize(model, options);
                modelString = Helpers.Helpers.updateFortunecookieBindNull(modelString);
                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, modelString);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            finally { }
        }
    }
}