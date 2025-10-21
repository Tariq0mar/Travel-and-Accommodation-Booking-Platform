using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

public static class ReviewFilterExtension
{
    public static IQueryable<Review> ApplyFilter(this IQueryable<Review> query, ReviewFilter? filter)
    {
        if (filter is null)
            return query;

        if (filter.HotelId.HasValue)
            query = query.Where(r => r.HotelId == filter.HotelId.Value);

        if (filter.UserId.HasValue)
            query = query.Where(r => r.UserId == filter.UserId.Value);

        if (filter.MinStarRating.HasValue)
            query = query.Where(r => r.StarRating >= filter.MinStarRating.Value);

        if (filter.MaxStarRating.HasValue)
            query = query.Where(r => r.StarRating <= filter.MaxStarRating.Value);

        if (filter.CreationDateFrom.HasValue)
            query = query.Where(r => r.CreatedAt >= filter.CreationDateFrom.Value);

        if (filter.CreationDateTo.HasValue)
            query = query.Where(r => r.CreatedAt <= filter.CreationDateTo.Value);

        return query;
    }
}