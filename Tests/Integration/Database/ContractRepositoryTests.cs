public class ContractRepositoryTests(IContractRepository repository)
{
    // WARNING!!! these are not reliable tests, they will fail, these were shortcuts I used for building a POC, these tests will need to be adjusted in order to be idempotent
    
    [Fact]
    public void Insert()
    {
        // Arrange
        var contract = FakeData.Contracts[0];

        // Act
        var id = repository.Insert(contract);

        // Assert
        Assert.True(id != Guid.Empty);
    }

    [Fact]
    public void Upsert()
    {
        // Arrange
        var contract = FakeData.Contracts[0];

        // Act
        var id = repository.Upsert(contract);

        // Assert
        Assert.True(id != Guid.Empty);
    }

    [Fact]
    public void Query()
    {
        // Arrange
        var command = new ContractQuery();

        // Act
        var result = repository.Query(command);

        // Assert
        Assert.True(result.Contracts.Count() > 0);
    }
}
