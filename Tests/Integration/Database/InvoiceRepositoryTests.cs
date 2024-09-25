public class InvoiceRepositoryTests(IInvoiceRepository repository)
{
    // WARNING!!! these are not reliable tests, they will fail, these were shortcuts I used for building a POC, these tests will need to be adjusted in order to be idempotent

    [Fact]
    public void Query()
    {
        // Arrange
        var command = new InvoiceQuery();

        // Act
        var result = repository.Query(command);

        // Assert
        Assert.True(result.Invoices.Count() > 0);
    }
}
