using Gov.Cscp.Victims.Public.Models;
using Gov.Cscp.Victims.Public.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Serilog;
using System;
using System.Linq;


namespace Gov.Cscp.Victims.Public.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class DynamicsBudgetProposalController : Controller
    {
        private readonly IDynamicsResultService _dynamicsResultService;
        private readonly ILogger _logger;

        public DynamicsBudgetProposalController(IDynamicsResultService dynamicsResultService)
        {
            this._dynamicsResultService = dynamicsResultService;
            _logger = Log.Logger;
        }

        [HttpGet("{businessBceid}/{userBceid}/{contractId}")]
        public async Task<IActionResult> GetBudgetProposal(string businessBceid, string userBceid, string contractId)
        {
            try
            {
                string requestJson = "{\"UserBCeID\":\"" + userBceid + "\",\"BusinessBCeID\":\"" + businessBceid + "\"}";
                string endpointUrl = "vsd_contracts(" + contractId + ")/Microsoft.Dynamics.CRM.vsd_GetCPUBudgetProposal";

                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, requestJson);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected error while getting budget proposal info 'vsd_GetCPUBudgetProposal'. Contract id = {contractId}. Source = CPU");
                return BadRequest();
            }
            finally { }
        }

        [HttpPost]
        public async Task<IActionResult> SetBudgetProposal([FromBody] BudgetProposalPost model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string messages = string.Join("\n", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                    _logger.Error(new Exception(messages), $"API call to 'SetBudgetProposal' made with invalid model state. Error is:\n{messages}. Source = CPU");
                    return BadRequest(ModelState);
                }

                string endpointUrl = "vsd_SetCPUOrgContracts";
                string modelString = System.Text.Json.JsonSerializer.Serialize(model);
                modelString = Helpers.Helpers.updateFortunecookieBindNull(modelString);
                modelString = Helpers.Helpers.removeNullsForBudgetProposal(modelString);

                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, modelString);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unexpected error while submitting budget proposal. Source = CPU");
                return BadRequest();
            }
            finally { }
        }
    }
}