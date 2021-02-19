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
    public class DynamicsRegisterNewUserController : Controller
    {
        private readonly IDynamicsResultService _dynamicsResultService;
        private readonly ILogger _logger;

        public DynamicsRegisterNewUserController(IDynamicsResultService dynamicsResultService)
        {
            this._dynamicsResultService = dynamicsResultService;
            _logger = Log.Logger;
        }


        [HttpPost]
        public async Task<IActionResult> RegisterNewUser([FromBody] RegisterNewUserPost model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
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
                _logger.Error(e, "Unexpected error while registering new user. Source = CPU");
                return BadRequest();
            }
            finally { }
        }
    }
}
