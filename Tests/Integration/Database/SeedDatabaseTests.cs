public class SeedDatabaseTests(SeedDatabase seedDatabase, IProgramRepository programRepository, IContractRepository contractRepository)
{
    // NOTE these are not actually tests, created here to cut corners, should be moved somewhere else after this POC is completed
    [Fact]
    public void Initialize()
    {
        seedDatabase.Seed();
    }

    [Fact]
    public void Clear_Fake_Data()
    {
        seedDatabase.Clear();
    }

    [Fact]
    public void Get_Approved_Programs()
    {
        var contractQuery = new ContractQuery();
        //contractQuery.
        var contracts = contractRepository.Query(contractQuery);

        var programQuery = new ProgramQuery();
        programQuery.StateCode = StateCode.Active;
        //programQuery.StatusCode
        var programs = programRepository.Query(programQuery);

        var approvedPrograms = programRepository.GetApproved();
    }
}