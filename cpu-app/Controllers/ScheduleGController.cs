using Gov.Cscp.Victims.Public.Background;
using Manager;
using Manager.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Convert = Manager.Contract.Convert;

namespace Gov.Cscp.Victims.Public.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ScheduleGController(
        IBackgroundTaskQueue taskQueue, IHostApplicationLifetime applicationLifetime, ILoggerFactory loggerFactory,
        ProgramHandlers programHandlers, ContractHandlers contractHandlers, ScheduleGHandlers scheduleGHandlers, TaskHandlers taskHandlers
    ) : Controller
    {
        private readonly CancellationToken _cancellationToken = applicationLifetime.ApplicationStopping;
        private readonly ILogger _logger = loggerFactory.CreateLogger<ScheduleGController>();

        [HttpGet("CreateScheduleGTasks/{quarter}")]
        public async Task<IActionResult> CreateScheduleGTasks(int quarter)
        {
            var quarterPeriod = Convert.ToQuarter(quarter);

            var contractQuery = new ContractQuery();
            contractQuery.StateCode = StateCode.Active;
            contractQuery.StatusCode = ContractStatusCode.DulyExecuted;
            contractQuery.NotNullCustomer = true;
            contractQuery.NotNullFiscalStartDate = true;
            contractQuery.NotNullFiscalEndDate = true;
            contractQuery.NotEqualType = ContractType.TuaCommunityAccountabilityPrograms;
            var contractResult = await contractHandlers.Handle(contractQuery, _cancellationToken);
            _logger.LogInformation("Found {0} contracts", contractResult.Contracts.Count());

            foreach (var contract in contractResult.Contracts)
            {
                var programQuery = new ProgramQuery();
                programQuery.StateCode = StateCode.Active;
                programQuery.StatusCode = ProgramStatusCode.Completed;
                programQuery.ContractId = contract.Id;
                var programResult = await programHandlers.Handle(programQuery, _cancellationToken);
                _logger.LogInformation("Found {0} programs", programResult.Programs.Count());

                foreach (var program in programResult.Programs)
                {
                    var scheduleGQuery = new ScheduleGQuery();
                    scheduleGQuery.ProgramId = program.Id;
                    scheduleGQuery.Quarter = quarterPeriod;
                    var scheduleGCount = (await scheduleGHandlers.Handle(scheduleGQuery, _cancellationToken))?.ScheduleGs.Count();
                    _logger.LogInformation("Found {0} schedule g's", scheduleGCount);

                    if (scheduleGCount == 0)
                    {
                        _logger.LogInformation(string.Format("Creating a new Schedule G for reporting period {0} and program '{1}'..", quarter, program.Id));
                        var scheduleG = new ScheduleG();
                        scheduleG.ServiceProviderId = contract.CustomerId;
                        scheduleG.ProgramId = program.Id;
                        scheduleG.ContractId = contract.Id;
                        scheduleG.Quarter = quarterPeriod;
                        scheduleG.Id = await scheduleGHandlers.Handle(scheduleG, _cancellationToken);

                        _logger.LogInformation(string.Format("Creating a new Task to Schedule G '{0}'..", scheduleG.Id));
                        var task = new Manager.Contract.Task();
                        task.Subject = string.Format("Schedule G - {0} - {1} Q{2}", program.Name, DateTime.Today.Year, quarter);
                        task.TaskTypeId = Constant.QuarterlyScheduleG;
                        task.ScheduledEnd = DateTime.Today.AddMonths(1);
                        task.RegardingObjectId = contract.Id;
                        task.ProgramId = program.Id;
                        task.ScheduleGId = scheduleG.Id;
                        task.Id = await taskHandlers.Handle(task, _cancellationToken);
                        _logger.LogInformation(string.Format("New task created with id '{0}'..", task.Id));
                    }
                }
            }

            return Json(quarter);
        }
    }
}
