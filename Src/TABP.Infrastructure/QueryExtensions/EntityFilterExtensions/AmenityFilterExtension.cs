using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

public static class AmenityFilterExtension
{
    public static IQueryable<Amenity> ApplyFilter(this IQueryable<Amenity> query, AmenityFilter? filter)
    {
        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(a => a.Name.Contains(filter.Name));

        return query;
    }
}