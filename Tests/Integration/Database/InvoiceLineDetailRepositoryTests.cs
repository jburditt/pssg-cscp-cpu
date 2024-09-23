public class InvoiceLineDetailRepositoryTests(IInvoiceLineDetailRepository repository)
{
    // WARNING!!! these are not reliable tests, they will fail, these were shortcuts I used for building a POC, these tests will need to be adjusted in order to be idempotent

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