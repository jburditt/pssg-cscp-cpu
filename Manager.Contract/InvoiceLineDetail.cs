namespace Manager.Contract;

public record InvoiceLineDetailQuery : IRequest<InvoiceLineDetailResult>
{
    public Guid? Id { get; set; }
    public Guid? InvoiceId { get; set; }
}

public record InvoiceLineDetailResult(IEnumerable<InvoiceLineDetail> InvoiceLineDetails);

public record InvoiceLineDetail : IDto
{
    public Guid Id { get; set; }
    public StateCode StateCode { get; set; }
    public InvoiceType? InvoiceType { get; set; }
    public ProgramUnit? ProgramUnit { get; set; }
    public YesNo? Approved { get; set; }
    public decimal? AmountSimple { get; set; }
    public TaxExemption? TaxExemption { get; set; }

    // References
    public Guid InvoiceId { get; set; }
    public Guid OwnerId { get; set; }
    public Guid? ProvinceStateId { get; set; }
}
