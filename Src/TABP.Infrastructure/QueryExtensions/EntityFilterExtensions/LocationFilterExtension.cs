using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

public static class LocationFilterExtension
{
    public static IQueryable<Location> ApplyFilter(this IQueryable<Location> query, LocationFilter? filter)
    {
        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.Country))
            query = query.Where(l => l.Country.Contains(filter.Country));

        if (!string.IsNullOrWhiteSpace(filter.City))
            query = query.Where(l => l.City.Contains(filter.City));

        return query;
    }
}