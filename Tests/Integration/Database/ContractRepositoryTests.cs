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

    [Fact]
    public void Delete()
    {
        // Arrange
        var id = new Guid("");

        // Act
        var result = repository.TryDelete(id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Clone()
    {
        // Arrange
        var command = new ContractQuery();
        command.StateCode = StateCode.Active;
        command.StatusCode = ContractStatusCode.DulyExecuted;
        command.CpuCloneFlag = true;

        // Act
        var result = repository.Query(command);
        Guid? id = null;
        foreach (var contract in result.Contracts)
        {
            if (!repository.IsCloned(contract.Id))
            {
                id = repository.Clone(contract);
            }
        }

        // Assert
        Assert.NotNull(id);
    }
}
