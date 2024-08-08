using Gov.Cscp.Victims.Public.Models;
using Gov.Cscp.Victims.Public.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Serilog;
using System;
using System.Linq;
using Xrm.Tools.WebAPI;
using Xrm.Tools.WebAPI.Results;
using System.Dynamic;


namespace Gov.Cscp.Victims.Public.Controllers
{

    [Route("api/[controller]")]
    [Authorize]
    public class DynamicsOrgController : Controller
    {
        private readonly IDynamicsResultService _dynamicsResultService;
        private readonly ILogger _logger;
        private readonly CRMWebAPI _api;

        public DynamicsOrgController(IDynamicsResultService dynamicsResultService, CRMWebAPI api)
        {
            this._dynamicsResultService = dynamicsResultService;
            _logger = Log.Logger;
            _api = api;
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            CRMGetListResult<ExpandoObject> test = null;

            try
            {
                test = await _api.GetList("bcgov_documenturls");
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unexpected error while getting test data. Source = CPU");
                return BadRequest();
            }

            return Json(test);

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrganizationPost model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string messages = string.Join("\n", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                    _logger.Error(new Exception(messages), $"API call to 'Create' made with invalid model state. Error is:\n{messages}\nSource = CPU");
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
                    string messages = string.Join("\n", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                    _logger.Error(new Exception(messages), $"API call to 'SetStaff' made with invalid model state. Error is:\n{messages}\nSource = CPU");
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