using Task = Manager.Contract.Task;

public class ScheduleGTests(IContractRepository contractRepository, IProgramRepository programRepository, IScheduleGRepository scheduleGRepository, ITaskRepository taskRepository, ILoggerFactory loggerFactory)
{
    private readonly ILogger<ScheduleGTests> _logger = loggerFactory.CreateLogger<ScheduleGTests>();

    // WARNING!!! these are not reliable tests, they will fail, these were shortcuts I used for building a POC, these tests will need to be adjusted in order to be idempotent

    [Fact]
    public void Query()
    {
        // Arrange
        var command = new ScheduleGQuery();
        command.Id = new Guid("");

        // Act
        var result = scheduleGRepository.Query(command);

        // Assert
        Assert.True(result.ScheduleGs.Count() > 0);
    }

    [Fact]
    public void Delete()
    {
        // Arrange
        var id = new Guid("");

        // Act
        var result = scheduleGRepository.TryDelete(id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Create_Schedule_Tasks()
    {
        var quarter = Quarter._1StQuarter;

        var contractQuery = new ContractQuery();
        contractQuery.StateCode = StateCode.Active;
        contractQuery.StatusCode = ContractStatusCode.DulyExecuted;
        contractQuery.NotNullCustomer = true;
        contractQuery.NotNullFiscalStartDate = true;
        contractQuery.NotNullFiscalEndDate = true;
        contractQuery.NotEqualType = ContractType.TuaCommunityAccountabilityPrograms;
        var contractResult = contractRepository.Query(contractQuery);
        _logger.LogInformation("Found {0} contracts", contractResult.Contracts.Count());

        foreach (var contract in contractResult.Contracts)
        {
            var programQuery = new ProgramQuery();
            programQuery.StateCode = StateCode.Active;
            programQuery.StatusCode = ProgramStatusCode.Completed;
            programQuery.ContractId = contract.Id;
            var programResult = programRepository.Query(programQuery);
            _logger.LogInformation("Found {0} programs", programResult.Programs.Count());

            foreach (var program in programResult.Programs)
            {
                var scheduleGQuery = new ScheduleGQuery();
                scheduleGQuery.ProgramId = program.Id;
                scheduleGQuery.Quarter = quarter;
                var scheduleGCount = scheduleGRepository.Query(scheduleGQuery)?.ScheduleGs.Count();
                _logger.LogInformation("Found {0} schedule g's", scheduleGCount);

                if (scheduleGCount == 0)
                {
                    _logger.LogInformation(string.Format("Creating a new Schedule G for reporting period {0} and program '{1}'..", quarter, program.Id));
                    var scheduleG = new ScheduleG();
                    scheduleG.ServiceProviderId = contract.CustomerId;
                    scheduleG.ProgramId = program.Id;
                    scheduleG.ContractId = contract.Id;
                    scheduleG.Quarter = quarter;
                    scheduleG.Id = scheduleGRepository.Insert(scheduleG);

                    _logger.LogInformation(string.Format("Creating a new Task to Schedule G '{0}'..", scheduleG.Id));
                    var task = new Task();
                    task.Subject = string.Format("Schedule G - {0} - {1} Q{2}", program.Name, DateTime.Today.Year, quarter);
                    task.TaskTypeId = Constant.QuarterlyScheduleG;
                    task.ScheduledEnd = DateTime.Today.AddMonths(1);
                    task.RegardingObjectId = contract.Id;
                    task.ProgramId = program.Id;
                    task.ScheduleGId = scheduleG.Id;
                    task.Id = taskRepository.Insert(task);
                    _logger.LogInformation(string.Format("New task created with id '{0}'..", task.Id));
                }
            }
        }
    }
}
