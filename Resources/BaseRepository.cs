using Database.Model;
using Microsoft.Xrm.Sdk;

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

    public virtual bool Delete<T>(T entity) where T : Entity
    {
        _databaseContext.DeleteObject(entity);
        _databaseContext.SaveChanges();
        return true;
    }
}