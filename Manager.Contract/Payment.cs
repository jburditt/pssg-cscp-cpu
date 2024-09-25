namespace Manager.Contract;

public enum PaymentStatusCode
{
    Canceled = 100000007,
    Failed = 100000003,
    NeedManualIntervention = 100000009,
    Negative = 100000006,
    OnHold = 100000008,
    Paid = 2,
    Sending = 100000001,
    Sent = 100000002,
    Voided = 100000010,
    Waiting = 100000000,
}

public record PaymentQuery : IRequest<PaymentResult>
{
    public Guid? ProgramId { get; set; }
    public Guid? ContractId { get; set; }
    public List<PaymentStatusCode>? ExcludeStatusCodes { get; set; }
}

public record PaymentResult(IEnumerable<Payment> Payments);

public record Payment : IDto
{
    public Guid Id { get; set; }
    public StateCode StateCode { get; set; }
    public decimal? PaymentTotal { get; set; }
}
