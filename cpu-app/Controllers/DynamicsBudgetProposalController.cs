using Gov.Cscp.Victims.Public.Models;
using Gov.Cscp.Victims.Public.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace Gov.Cscp.Victims.Public.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class DynamicsBudgetProposalController : Controller
    {
        private readonly IDynamicsResultService _dynamicsResultService;

        public DynamicsBudgetProposalController(IDynamicsResultService dynamicsResultService)
        {
            this._dynamicsResultService = dynamicsResultService;
        }

        [HttpGet("{businessBceid}/{userBceid}/{contractId}")]
        public async Task<IActionResult> GetBudgetProposal(string businessBceid, string userBceid, string contractId)
        {
            try
            {
                // convert the parameters to a json string
                string requestJson = "{\"UserBCeID\":\"" + userBceid + "\",\"BusinessBCeID\":\"" + businessBceid + "\"}";
                // set the endpoint action
                string endpointUrl = "vsd_contracts(" + contractId + ")/Microsoft.Dynamics.CRM.vsd_GetCPUBudgetProposal";

                // get the response
                DynamicsResult result = await _dynamicsResultService.Post(endpointUrl, requestJson);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            finally { }
        }

        [HttpPost]
        public async Task<IActionResult> SetBudgetProposal([FromBody] BudgetProposalPost model)
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
                modelString = Helpers.Helpers.removeNullsForBudgetProposal(modelString);

                DynamicsResult result = await _dynamicsResultService.Post(endpointUrl, modelString);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            finally { }
        }
    }
}