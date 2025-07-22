using TABP.Domain.QueryFilters;

namespace TABP.Infrastructure.QueryExtensions;

public static class PaginationExtensions
{
    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, PaginationRecord pagination)
    {
        var skip = (pagination.PageNumber - 1) * pagination.PageSize;
        return query.Skip(skip).Take(pagination.PageSize);
    }
}