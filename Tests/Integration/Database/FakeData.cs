public class FakeData
{
    public static List<Contract> Contracts = new List<Contract>
    {
        new Contract
        {
            Id = new Guid("091bd597-3938-4d2d-97e6-c3d31b7c0800"),
            StateCode = StateCode.Active,
        },
        new Contract
        {
            Id = new Guid("9833f1e7-6690-43b7-bbaa-dfed630fa13e"),
            StateCode = StateCode.Active,
        }
    };
}
