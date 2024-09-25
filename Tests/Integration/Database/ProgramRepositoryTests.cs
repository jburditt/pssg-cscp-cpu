public class ProgramRepositoryTests(IProgramRepository programRepository)
{
    // WARNING!!! these are not reliable tests, they will fail, these were shortcuts I used for building a POC, these tests will need to be adjusted in order to be idempotent

    [Fact]
    public void Upsert()
    {
        // Arrange
        var program = FakeData.Programs[0];

        // Act
        var id = programRepository.Upsert(program);

        // Assert
        Assert.True(id != Guid.Empty);
    }

    // TODO not a consistent test yet
    [Fact]
    public void Query()
    {
        // Arrange
        var programQuery = new ProgramQuery();
        programQuery.Id = FakeData.Programs[0].Id;

        // Act
        var result = programRepository.Query(programQuery);

        // Assert
        Assert.True(result.Programs.Count() > 0);
    }

    [Fact]
    public void Delete()
    {
        // Arrange
        var program = FakeData.Programs[0];

        // Act
        var result = programRepository.Delete(program.Id);

        // Assert
        Assert.True(result);
    }
}
