﻿namespace Manager;

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
    public async Task<Guid> Handle(InsertCommand<TDto> command, CancellationToken cancellationToken)
    {
        var results = repository.Insert(command.Payload);
        return await Task.FromResult(results);
    }

    public async Task<Guid> Handle(UpsertCommand<TDto> command, CancellationToken cancellationToken)
    {
        var results = repository.Upsert(command.Payload);
        return await Task.FromResult(results);
    }

    public async Task<bool> Handle(TryDeleteCommand command, CancellationToken cancellationToken)
    {
        var results = repository.TryDelete(command.Id);
        return await Task.FromResult(results);
    }

    public async Task<bool> Handle(TryDeleteCommand<TDto> command, CancellationToken cancellationToken)
    {
        var results = repository.TryDelete(command.Payload);
        return await Task.FromResult(results);
    }

    public async Task<bool> Handle(DeleteCommand command, CancellationToken cancellationToken)
    {
        var results = repository.Delete(command.Id);
        return await Task.FromResult(results);
    }
}

public record InsertCommand<TDto>(TDto dto) : PayloadCommand<TDto>(dto) { }
public record UpsertCommand<TDto>(TDto dto) : PayloadCommand<TDto>(dto) { }
public record TryDeleteCommand<TDto>(TDto dto) : PayloadCommand<TDto>(dto) { }
public record TryDeleteCommand(Guid Id) : IdCommand(Id) { }
public record DeleteCommand(Guid Id) : IdCommand(Id) { }