namespace Manager;

// Query and Base Handler
public class QueryBaseHandlers<TRepository, TDto, TQuery, TResult>(TRepository repository) : BaseHandlers<TRepository, TDto>(repository)
    where TRepository : IQueryRepository<TQuery, TResult>, IBaseRepository<TDto>
    where TDto : IDto
{
    public async Task<TResult> Handle(TQuery query, CancellationToken cancellationToken)
    {
        var results = repository.Query(query);
        return await Task.FromResult(results);
    }
}

// Only Base Handler
public class BaseHandlers<TRepository, TDto>(TRepository repository)
    where TRepository : IBaseRepository<TDto>
    where TDto : IDto
{
    public async Task<Guid> Handle(TDto dto, CancellationToken cancellationToken)
    {
        var results = repository.Insert(dto);
        return await Task.FromResult(results);
    }
}
