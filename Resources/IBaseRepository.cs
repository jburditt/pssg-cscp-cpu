﻿namespace Resources;

public interface IBaseRepository<TDto> where TDto : IDto
{
    Guid Insert(TDto dto);
    Guid Upsert(TDto dto);
    bool TryDelete(Guid id);
    bool TryDelete(TDto dto);
}
