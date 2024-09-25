using Manager.Contract;

public enum IsoCurrencyCode
{
    CAD
}

public record FindCurrencyQuery : IRequest<FindCurrencyResult>
{
    public StateCode? StateCode { get; set; }
    public string? IsoCurrencyCode { get; set; }
}

public record FindCurrencyResult(Currency Currency);


public record CurrencyQuery : IRequest<CurrencyResult>
{
    public StateCode? StateCode { get; set; }
    public string? IsoCurrencyCode { get; set; }
}

public record CurrencyResult(IEnumerable<Currency> Currencies);

public record Currency : IDto
{
    public Guid Id { get; set; }
    public StateCode StateCode { get; set; }
    public StatusCode? StatusCode { get; set; }
    public required string IsoCurrencyCode { get; set; }
}
