public class ContractRepositoryTests(IContractRepository repository)
{
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

        var test = JsonConvert.SerializeObject(result);
        // Assert
        Assert.True(result.Contracts.Count() > 0);
    }
}
