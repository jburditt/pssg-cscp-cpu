namespace Resources;

public interface ICurrencyRepository
{
    CurrencyResult Query();
}

public enum StateCode
{
    Active = 0,
    Inactive = 1
}

public enum StatusCode
{
    Active = 0,
    Inactive = 1
}

public record Currency(StateCode StateCode, StatusCode StatusCode, string IsoCurrencyCode);

public record CurrencyResult(IEnumerable<Currency> Currencies);