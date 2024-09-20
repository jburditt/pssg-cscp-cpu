namespace Resources;

public interface IBaseRepository<TDto>
    where TDto : IDto
{
    Guid Insert(TDto dto);
    Guid Upsert(TDto dto);
    bool Delete(Guid id);
    bool Delete(TDto dto);
}
