public class SeedDatabase(DatabaseContext databaseContext, IContractRepository contractRepository, IProgramRepository programRepository)
{
    public void Seed()
    {
        // Seed the database
        Guid id;

        foreach (var contract in FakeData.Contracts)
        {
            try
            {
                id = contractRepository.Insert(contract);
                Console.WriteLine($"Inserted contract with id {id}");
            } catch { /* glup */ }
        }

        foreach (var program in FakeData.Programs)
        {
            try
            {
                id = programRepository.Upsert(program);
                Console.WriteLine($"Inserted program with id {id}");
            }
            catch { /* glup */ }
        }
    }

    public void Clear()
    {
        // Clear all the fake data in database
        bool isDeleted;
        foreach (var contract in FakeData.Contracts)
        {
            try
            {
                isDeleted = contractRepository.TryDelete(contract.Id);
                Console.WriteLine($"Deleted contract with id {contract.Id} {isDeleted}");
            }
            catch { /* glup */ }
        }

        foreach (var program in FakeData.Programs)
        {
            try
            {
                isDeleted = programRepository.Delete(program.Id);
                Console.WriteLine($"Deleted program with id {program.Id} {isDeleted}");
            }
            catch { /* glup */ }
        }
    }
}
