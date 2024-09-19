namespace Resources;

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