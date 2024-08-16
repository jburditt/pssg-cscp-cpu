using Resources;

namespace Manager;

public class CurrencyHandlers(ICurrencyRepository currentRepository) //: IRequestHandler<CurrencyResult>
{
    public async Task<CurrencyResult> Handle(CancellationToken cancellationToken = default)
    {
        return currentRepository.Query();
    }
}
