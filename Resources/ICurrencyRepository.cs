using Database.Model;
using Manager.Contract;

namespace Resources;

public interface ICurrencyRepository
{
    CurrencyResult Query();
}

public record CurrencyResult(IEnumerable<Currency> Currencies);