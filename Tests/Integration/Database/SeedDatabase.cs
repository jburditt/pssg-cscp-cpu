public class SeedDatabase(DatabaseContext databaseContext, IContractRepository contractRepository)
{
    public void Initialize()
    {
        // Seed the database
        Guid id;
        foreach (var contract in FakeData.Contracts)
        {
            id = contractRepository.Insert(contract);
            Console.WriteLine($"Inserted contract with id {id}");
        }
    }
}
