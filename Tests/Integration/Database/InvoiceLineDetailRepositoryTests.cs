public class InvoiceLineDetailRepositoryTests(IInvoiceLineDetailRepository repository)
{
    [Fact]
    public void Insert()
    {
        // Arrange
        var invoiceLineDetail = FakeData.InvoiceLineDetails[0];

        // Act
        var id = repository.Insert(invoiceLineDetail);

        // Assert
        Assert.True(id != Guid.Empty);
    }

    [Fact]
    public void Query()
    {
        // Arrange
        var command = new InvoiceLineDetailQuery();
        command.Id = FakeData.InvoiceLineDetails[0].Id;

        // Act
        var result = repository.Query(command);

        // Assert
        Assert.True(result.Count() > 0);
    }

    [Fact]
    public void Delete()
    {
        // Arrange
        var id = FakeData.InvoiceLineDetails[0].Id;

        // Act
        var result = repository.Delete(id);

        // Assert
        Assert.True(result);
    }
}