public class ScheduleGTests(IContractRepository contractRepository, IProgramRepository programRepository, IScheduleGRepository scheduleGRepository)
{
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

        foreach (var contract in contractResult.Contracts)
        {
            var programQuery = new ProgramQuery();
            programQuery.StateCode = StateCode.Active;
            programQuery.StatusCode = ProgramStatusCode.Completed;
            programQuery.ContractId = contract.Id;
            var programResult = programRepository.Query(programQuery);

            foreach (var program in programResult.Programs)
            {
                var scheduleGQuery = new ScheduleGQuery();
                scheduleGQuery.ProgramId = program.Id;
                scheduleGQuery.Quarter = quarter;
                var scheduleGExists = scheduleGRepository.Query(scheduleGQuery)?.ScheduleGs.Count() > 0;
                if (!scheduleGExists)
                {
                    //var scheduleG = new ScheduleG();
                    //scheduleG.ServiceProviderId = contract.CustomerId;
                    //scheduleG.ProgramId = program.Id;
                    //scheduleG.ContractId = contract.Id;
                    //scheduleG.Quarter = quarter;
                    //scheduleG.Id = scheduleGRepository.Insert(scheduleG);

                    //var scheduleGTask = new ScheduleGTask();
                }
            }
        }
    }
}
