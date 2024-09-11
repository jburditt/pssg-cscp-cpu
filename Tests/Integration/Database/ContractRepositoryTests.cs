using Newtonsoft.Json;

public class ContractRepositoryTests(IContractRepository contractRepository)
{
    //[Fact]
    //public void Insert()
    //{
    //    // Arrange
    //    var contract = FakeData.Contracts[0];

    //    // Act
    //    var id = contractRepository.Insert(contract);

    //    // Assert
    //    Assert.True(id != Guid.Empty);
    //}

    //[Fact]
    //public void Upsert()
    //{
    //    // Arrange
    //    var contract = FakeData.Contracts[0];

    //    // Act
    //    var id = contractRepository.Upsert(contract);

    //    // Assert
    //    Assert.True(id != Guid.Empty);
    //}

    [Fact]
    public void Query()
    {
        // Arrange
        var command = new ContractQuery();

        // Act
        var result = contractRepository.Query(command);

        var test = JsonConvert.SerializeObject(result);
        // Assert
        Assert.True(result.Contracts.Count() > 0);
    }
}
