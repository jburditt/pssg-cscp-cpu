namespace Manager;

// Query, Find, and Base Handler
public class FindQueryBaseHandlers<TRepository, TDto, TFindQuery, TFindResult, TQuery, TResult> : BaseHandlers<TRepository, TDto>
    where TRepository : IFindRepository<TFindQuery, TFindResult>, IQueryRepository<TQuery, TResult>, IBaseRepository<TDto>
    where TDto : IDto
{
    protected readonly TRepository _repository;

    public FindQueryBaseHandlers(TRepository repository) : base(repository)
    {
        _repository = repository;
    }

    public async Task<TFindResult> Handle(TFindQuery query, CancellationToken cancellationToken)
    {
        var results = _repository.FirstOrDefault(query);
        return await Task.FromResult(results);
    }

    public async Task<TResult> Handle(TQuery query, CancellationToken cancellationToken)
    {
        var results = _repository.Query(query);
        return await Task.FromResult(results);
    }
}

// Query and Base Handler
public class QueryBaseHandlers<TRepository, TDto, TQuery, TResult> : BaseHandlers<TRepository, TDto>
    where TRepository : IQueryRepository<TQuery, TResult>, IBaseRepository<TDto>
    where TDto : IDto
{
    protected readonly TRepository _repository;

    public QueryBaseHandlers(TRepository repository) : base(repository) 
    {
        _repository = repository;
    }

    public async Task<TResult> Handle(TQuery query, CancellationToken cancellationToken)
    {
        var results = _repository.Query(query);
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
