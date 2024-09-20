public class TaskTests(ITaskRepository repository)
{
    [Fact]
    public void Delete()
    {
        // Arrange
        var id = new Guid("");

        // Act
        var result = repository.Delete(id);
        
        // Assert
        Assert.True(result);
    }
}
