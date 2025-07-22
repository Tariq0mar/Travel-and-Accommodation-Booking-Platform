using System.Linq.Expressions;
using TABP.Domain.Enums;
using TABP.Domain.QueryFilters;

namespace TABP.Infrastructure.QueryExtensions;

public static class SortingExtensions
{
    public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, string? sortString)
    {
        var criteria = ParsingSortingString.Parse(sortString);
        
        if (!criteria.Any())
            return query;

        var parameter = Expression.Parameter(typeof(T), "x");
        var operations = new List<Func<IQueryable<T>, IQueryable<T>>>();
        bool isFirst = true;

        foreach (var (propertyName, direction) in criteria)
        {
            var property = Expression.PropertyOrField(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);
            var methodName = GetMethodName(direction, isFirst);
            isFirst = false;

            operations.Add(q =>
            {
                var call = Expression.Call(
                    typeof(Queryable),
                    methodName,
                    new[] { typeof(T), property.Type },
                    q.Expression,
                    Expression.Quote(lambda));

                return (IOrderedQueryable<T>)q.Provider.CreateQuery<T>(call);
            });
        }

        foreach (var op in operations)
            query = op(query);

        return query;
    }

    private static string GetMethodName(SortDirection dir, bool isFirst) =>
        (dir, isFirst) switch
        {
            (SortDirection.Asc, true) => "OrderBy",
            (SortDirection.Desc, true) => "OrderByDescending",
            (SortDirection.Asc, false) => "ThenBy",
            (SortDirection.Desc, false) => "ThenByDescending",
            _ => throw new InvalidOperationException()
        };
}
