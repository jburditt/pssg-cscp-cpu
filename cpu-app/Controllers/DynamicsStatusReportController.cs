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
    public class DynamicsStatusReportController : Controller
    {
        private readonly IDynamicsResultService _dynamicsResultService;
        private readonly ILogger _logger;

        public DynamicsStatusReportController(IDynamicsResultService dynamicsResultService)
        {
            this._dynamicsResultService = dynamicsResultService;
            _logger = Log.Logger;
        }

        [HttpGet("{businessBceid}/{userBceid}/{taskId}")]
        public async Task<IActionResult> GetQuestions(string businessBceid, string userBceid, string taskId)
        {
            try
            {
                string requestJson = "{\"UserBCeID\":\"" + userBceid + "\",\"BusinessBCeID\":\"" + businessBceid + "\"}";
                string endpointUrl = "tasks(" + taskId + ")/Microsoft.Dynamics.CRM.vsd_GetCPUMonthlyStatisticsQuestions";

                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, requestJson);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected error while getting stats questions info 'vsd_GetCPUMonthlyStatisticsQuestions'. Task id = {taskId}. Source = CPU");
                return BadRequest();
            }
            finally { }
        }
        [HttpGet("monthly_stats/{businessBceid}/{userBceid}/{contractId}")]
        public async Task<IActionResult> GetMonthlyStats(string businessBceid, string userBceid, string contractId)
        {
            try
            {
                string requestJson = "{\"UserBCeID\":\"" + userBceid + "\",\"BusinessBCeID\":\"" + businessBceid + "\"}";
                string endpointUrl = "vsd_contracts(" + contractId + ")/Microsoft.Dynamics.CRM.vsd_GetCPUMonthlyStatistics";

                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, requestJson);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected error while getting monthly stats info 'vsd_GetCPUMonthlyStatistics'. Contract id = {contractId}. Source = CPU");
                return BadRequest();
            }
            finally { }
        }
        [HttpGet("data_collection/{businessBceid}/{userBceid}/{dataCollectionId}")]
        public async Task<IActionResult> GetSubmittedReport(string businessBceid, string userBceid, string dataCollectionId)
        {
            try
            {
                string requestJson = "{\"UserBCeID\":\"" + userBceid + "\",\"BusinessBCeID\":\"" + businessBceid + "\"}";
                string endpointUrl = "vsd_datacollections(" + dataCollectionId + ")/Microsoft.Dynamics.CRM.vsd_GetCPUMonthlyStatisticsAnswers";

                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, requestJson);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected error while getting submitted stats info 'vsd_GetCPUMonthlyStatisticsAnswers'. Data collection id = {dataCollectionId}. Source = CPU");
                return BadRequest();
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
                    _logger.Error(new Exception(ModelState.ToString()), $"API call to 'AnswerQuestions' made with invalid model state. Error is:\n{ModelState}. Source = CPU");
                    return BadRequest(ModelState);
                }

                string endpointUrl = "tasks(" + taskId + ")/Microsoft.Dynamics.CRM.vsd_SetCPUMonthlyStatisticsAnswers";
                string modelString = System.Text.Json.JsonSerializer.Serialize(model);
                modelString = Helpers.Helpers.updateFortunecookieBindNull(modelString);
                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, modelString);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unexpected error while submitting stats answers. Source = CPU");
                return BadRequest();
            }
            finally { }
        }
    }
}