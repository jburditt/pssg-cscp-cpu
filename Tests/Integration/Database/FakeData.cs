public static class FakeData
{
    public static List<Contract> Contracts = new List<Contract>
    {
        new Contract
        {
            Id = new Guid("ce4f37e8-7043-41b2-a0e7-23fb93fa53c9"),
            StateCode = StateCode.Active,
            StatusCode = ContractStatusCode.DulyExecuted,
            MethodOfPayment = MethodOfPayment.Cheque,
            ContractType = ContractType.TuaVictimServicesVawp,
            ProgramId = new Guid("05dcfc76-77d1-471f-9b7a-a79332186fac"),
        },
        new Contract
    public static List<Manager.Contract.Program> Programs = new List<Manager.Contract.Program>
        {
        // GetApprovedProgram Programs must not have StatusCode.Draft, ApplicationInfoSent, or ApplicationInfoReceived
        // Type cannot be TuaCommunityAccountabilityPrograms
        new Manager.Contract.Program
        {
            Id = new Guid("05dcfc76-77d1-471f-9b7a-a79332186fac"),
            StateCode = StateCode.Active,
            StatusCode = ProgramStatusCode.Completed,
            ContractId = Contracts[0].Id,
            ContractName = "123456-23",
            OwnerId = OwnerIds[0],
            BudgetProposalSignatureDate = new DateTime(2021, 12, 1, 10, 34, 19),
            ProvinceState = "British Columbia",
        },
    };
}
