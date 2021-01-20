using Gov.Cscp.Victims.Public.Models;
using Gov.Cscp.Victims.Public.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class DynamicsStatusReportController : Controller
    {
        private readonly IDynamicsResultService _dynamicsResultService;

        public DynamicsStatusReportController(IDynamicsResultService dynamicsResultService)
        {
            this._dynamicsResultService = dynamicsResultService;
        }

        [HttpGet("{businessBceid}/{userBceid}/{taskId}")]
        public async Task<IActionResult> GetQuestions(string businessBceid, string userBceid, string taskId)
        {
            try
            {
                // convert the parameters to a json string
                string requestJson = "{\"UserBCeID\":\"" + userBceid + "\",\"BusinessBCeID\":\"" + businessBceid + "\"}";
                // set the endpoint action
                string endpointUrl = "tasks(" + taskId + ")/Microsoft.Dynamics.CRM.vsd_GetCPUMonthlyStatisticsQuestions";

                // get the response
                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, requestJson);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            finally { }
        }
        [HttpGet("monthly_stats/{businessBceid}/{userBceid}/{contractId}")]
        public async Task<IActionResult> GetMonthlyStats(string businessBceid, string userBceid, string contractId)
        {
            try
            {
                // convert the parameters to a json string
                string requestJson = "{\"UserBCeID\":\"" + userBceid + "\",\"BusinessBCeID\":\"" + businessBceid + "\"}";
                // set the endpoint action
                string endpointUrl = "vsd_contracts(" + contractId + ")/Microsoft.Dynamics.CRM.vsd_GetCPUMonthlyStatistics";

                // get the response
                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, requestJson);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            finally { }
        }
        [HttpGet("data_collection/{businessBceid}/{userBceid}/{dataCollectionId}")]
        public async Task<IActionResult> GetSubmittedReport(string businessBceid, string userBceid, string dataCollectionId)
        {
            try
            {
                // convert the parameters to a json string
                string requestJson = "{\"UserBCeID\":\"" + userBceid + "\",\"BusinessBCeID\":\"" + businessBceid + "\"}";
                // set the endpoint action
                string endpointUrl = "vsd_datacollections(" + dataCollectionId + ")/Microsoft.Dynamics.CRM.vsd_GetCPUMonthlyStatisticsAnswers";

                // get the response
                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, requestJson);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            finally { }
        }

        [HttpPost("{taskId}")]
        public async Task<IActionResult> AnswerQuestions([FromBody] MonthlyStatisticsAnswers model, string taskId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string endpointUrl = "tasks(" + taskId + ")/Microsoft.Dynamics.CRM.vsd_SetCPUMonthlyStatisticsAnswers";
                // make options for the json serializer
                // JsonSerializerOptions options = new JsonSerializerOptions();
                // options.IgnoreNullValues = true;
                // turn the model into a string
                string modelString = System.Text.Json.JsonSerializer.Serialize(model);
                modelString = Helpers.Helpers.updateFortunecookieBindNull(modelString);
                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, modelString);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            finally { }
        }
    }
}