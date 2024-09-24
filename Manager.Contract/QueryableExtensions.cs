public static class QueryableExtensions
{
    public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
    {
        if (condition)
        {
            return source.Where(predicate);
        }
        else
        {
            return source;
        }
    }

    public static IQueryable<TSource> WhereIfNotIn<TSource, TValue>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, TValue>> valueSelector, IEnumerable<TValue> values)
    {
        if (condition)
        {
            return source.WhereNotIn(valueSelector, values);
        }
        else
        {
            return source;
        }
    }

    /// <summary>
    /// Filters a sequence for elements with a property matching a predefined list of values (`in` filter)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source">The source query</param>
    /// <param name="valueSelector">The value selector to filter by</param>
    /// <param name="values">The list of values to include</param>
    /// <returns>The query with the added filter</returns>
    public static IQueryable<TSource> WhereIn<TSource, TValue>(this IQueryable<TSource> source, Expression<Func<TSource, TValue>> valueSelector, IEnumerable<TValue> values)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(valueSelector);
        ArgumentNullException.ThrowIfNull(values);

        var element = valueSelector.Parameters.Single();
        var body = values.Select(v => Expression.Equal(valueSelector.Body, Expression.Constant(v))).Aggregate(Expression.OrElse);

        var lambda = Expression.Lambda<Func<TSource, bool>>(body, element);

        return source.Where(lambda);
    }

    /// <summary>
    /// Filters a sequence for elements with a property not matching a predefined list of values (`not in` filter)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source">The source query</param>
    /// <param name="valueSelector">The value selector to filter by</param>
    /// <param name="values">The list of values to exclude</param>
    /// <returns>The query with the added filter</returns>
    public static IQueryable<TSource> WhereNotIn<TSource, TValue>(this IQueryable<TSource> source, Expression<Func<TSource, TValue>> valueSelector, IEnumerable<TValue> values)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(valueSelector);
        ArgumentNullException.ThrowIfNull(values);

        var element = valueSelector.Parameters.Single();
        var body = values.Select(v => Expression.NotEqual(valueSelector.Body, Expression.Constant(v))).Aggregate(Expression.AndAlso);

        var lambda = Expression.Lambda<Func<TSource, bool>>(body, element);

        return source.Where(lambda);
    }
}