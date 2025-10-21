using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

public static class HotelFilterExtension
{
    public static IQueryable<Hotel> ApplyFilter(this IQueryable<Hotel> query, HotelFilter? filter)
    {
        if (filter is null)
            return query;

        if (filter.LocationId.HasValue)
            query = query.Where(h => h.LocationId == filter.LocationId.Value);

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(h => h.Name.Contains(filter.Name));

        if (filter.MinStarRating.HasValue)
            query = query.Where(h => h.StarRating >= filter.MinStarRating.Value);

        if (filter.MaxStarRating.HasValue)
            query = query.Where(h => h.StarRating <= filter.MaxStarRating.Value);

        if (filter.CreationDateFrom.HasValue)
            query = query.Where(h => h.CreatedAt >= filter.CreationDateFrom.Value);

        if (filter.CreationDateTo.HasValue)
            query = query.Where(h => h.CreatedAt <= filter.CreationDateTo.Value);

        return query;
    }
}