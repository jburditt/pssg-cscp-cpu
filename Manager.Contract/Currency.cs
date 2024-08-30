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

public record Currency(Guid Id, StateCode StateCode, StatusCode StatusCode, string IsoCurrencyCode);
