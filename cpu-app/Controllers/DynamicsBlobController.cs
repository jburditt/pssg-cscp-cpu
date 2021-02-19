using Gov.Cscp.Victims.Public.Models;
using Gov.Cscp.Victims.Public.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Threading.Tasks;
using System;

namespace Gov.Cscp.Victims.Public.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class DynamicsBlobController : Controller
    {
        private readonly IDynamicsResultService _dynamicsResultService;
        private readonly ILogger _logger;

        public DynamicsBlobController(IDynamicsResultService dynamicsResultService)
        {
            this._dynamicsResultService = dynamicsResultService;
            _logger = Log.Logger;
        }

        [HttpGet("{businessBceid}/{userBceid}")]
        public async Task<IActionResult> GetBlob(string userBceid, string businessBceid)
        {
            try
            {
                string requestJson = "{\"UserBCeID\":\"" + userBceid + "\",\"BusinessBCeID\":\"" + businessBceid + "\"}";
                string endpointUrl = "vsd_GetCPUOrgContracts";

                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, requestJson);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unexpected error while getting contract info 'vsd_GetCPUOrgContracts'. Source = CPU");
                return BadRequest();
            }
            finally { }
        }
    }
}