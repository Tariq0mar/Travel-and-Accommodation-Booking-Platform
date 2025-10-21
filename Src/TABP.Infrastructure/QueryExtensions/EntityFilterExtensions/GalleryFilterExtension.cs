using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

public static class GalleryFilterExtension
{
    public static IQueryable<Gallery> ApplyFilter(this IQueryable<Gallery> query, GalleryFilter? filter)
    {
        if (filter is null)
            return query;

        if (filter.CreatedAtFrom.HasValue)
            query = query.Where(g => g.CreatedAt >= filter.CreatedAtFrom.Value);

        if (filter.CreatedAtTo.HasValue)
            query = query.Where(g => g.CreatedAt <= filter.CreatedAtTo.Value);

        return query;
    }
}