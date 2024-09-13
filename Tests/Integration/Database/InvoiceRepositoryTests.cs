public class InvoiceRepositoryTests(IInvoiceRepository repository)
{
    // WARNING!!! these are not reliable tests, they will fail, these were shortcuts I used for building a POC, these tests will need to be adjusted in order to be idempotent

    // NOTE verify your changes with https://cscp-vs.dev.jag.gov.bc.ca/api/data/v9.0/vsd_invoicelinedetails?$filter=vsd_invoicelinedetailid eq '00000000-e81b-ec11-b82d-00505683fbf4'

    //[Fact]
    //public void Insert()
    //{
    //    // Arrange
    //    var contract = FakeData.Invoices[0];

    //    // Act
    //    var id = repository.Insert(contract);

    //    // Assert
    //    Assert.True(id != Guid.Empty);
    //}

    //[Fact]
    //public void Upsert()
    //{
    //    // Arrange
    //    var contract = FakeData.Contracts[0];

    //    // Act
    //    var id = repository.Upsert(contract);

    //    // Assert
    //    Assert.True(id != Guid.Empty);
    //}

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
