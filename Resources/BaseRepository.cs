namespace Resources;

public abstract class BaseRepository<TEntity, TDto> 
    where TEntity : Entity
    where TDto : IDto
{
    protected readonly DatabaseContext _databaseContext;
    protected readonly IMapper _mapper;

    public BaseRepository(DatabaseContext databaseContext, IMapper mapper)
    {
        _databaseContext = databaseContext;
        _mapper = mapper;
    }

    public virtual Guid Insert(TDto dto)
    {
        var entity = Map(dto);
        _databaseContext.AddObject(entity);
        Save();
        return entity.Id;
    }

    public virtual Guid Upsert(TDto dto)
    {
        var entity = Map(dto);
        var existingEntity = _databaseContext
            .CreateQuery<TEntity>()
            .FirstOrDefault(x => x.Id == entity.Id);
        if (existingEntity != null)
        {
            _databaseContext.Detach(existingEntity);
            _databaseContext.Attach(entity);
            _databaseContext.UpdateObject(entity);
        }
        else
        {
            _databaseContext.AddObject(entity);
        }
        _databaseContext.SaveChanges();
        return entity.Id;
    }

    public virtual bool Delete(Guid id)
    {
        var dto = Activator.CreateInstance<TDto>();
        dto.Id = id;
        return Delete(dto);
    }

    public virtual bool Delete(TDto dto)
    {
        var entity = Map(dto);
        _databaseContext.Attach(entity);
        _databaseContext.DeleteObject(entity);
        Save();
        return true;
    }

    private TEntity Map(TDto dto)
    {
        return _mapper.Map<TEntity>(dto);
    }

    private void Save()
    {
        _databaseContext.SaveChanges();
    }
}

[Obsolete("Use BaseRepository<TEntity, TDto> instead")]
public abstract class BaseRepository
{
    protected readonly DatabaseContext _databaseContext;

    public BaseRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public virtual Guid Insert<T>(T entity) where T : Entity
    {
        _databaseContext.AddObject(entity);
        _databaseContext.SaveChanges();
        return entity.Id;
    }

    public virtual Guid Upsert<T>(T entity) where T : Entity
    {
        var existingEntity = _databaseContext
            .CreateQuery<T>()
            .FirstOrDefault(x => x.Id == entity.Id);
        if (existingEntity != null)
        {
            _databaseContext.Detach(existingEntity);
            _databaseContext.Attach(entity);
            _databaseContext.UpdateObject(entity);
        }
        else
        {
            _databaseContext.AddObject(entity);
        }
        _databaseContext.SaveChanges();
        return entity.Id;
    }

    public virtual bool Delete<T>(T entity) where T : Entity
    {
        _databaseContext.DeleteObject(entity);
        _databaseContext.SaveChanges();
        return true;
    }

    public virtual bool TryDelete<T>(T entity) where T : Entity
    {
        try
        {
            _databaseContext.DeleteObject(entity);
            _databaseContext.SaveChanges();
            return true;
        }
        catch 
        { 
            return false; 
        }
    }
}