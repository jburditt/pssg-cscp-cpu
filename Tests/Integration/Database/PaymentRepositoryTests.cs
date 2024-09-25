public class PaymentRepositoryTests(IPaymentRepository paymentRepository)
{
    [Fact]
    public void Query()
    {
        // Arrange
        var paymentQuery = new PaymentQuery();
        paymentQuery.ExcludeStatusCodes = new List<PaymentStatusCode> { PaymentStatusCode.Voided, PaymentStatusCode.Paid, PaymentStatusCode.Waiting, PaymentStatusCode.Sending, PaymentStatusCode.Sent };

        // Act
        var result = paymentRepository.Query(paymentQuery);

        // Assert
        Assert.True(result.Payments.Count() > 0);
    }
}
