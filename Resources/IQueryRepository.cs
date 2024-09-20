namespace Resources;

public interface IQueryRepository<TQuery, TResult>
{
    TResult Query(TQuery query);
}
