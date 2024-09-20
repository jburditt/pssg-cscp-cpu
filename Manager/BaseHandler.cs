namespace Manager;

public class BaseHandler<TRepository, TQuery, TResult>(TRepository repository) : IRequestHandler<TQuery, TResult> where TRepository : IQueryRepository<TQuery, TResult>
{
    public async Task<TResult> Handle(TQuery query, CancellationToken cancellationToken)
    {
        var results = repository.Query(query);
        return await Task.FromResult(results);
    }
}
