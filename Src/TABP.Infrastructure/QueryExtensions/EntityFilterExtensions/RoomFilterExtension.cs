using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

public static class RoomFilterExtension
{
    public static IQueryable<Room> ApplyFilter(this IQueryable<Room> query, RoomFilter? filter)
    {
        if (filter is null)
            return query;

        if (filter.HotelId.HasValue)
            query = query.Where(r => r.HotelId == filter.HotelId.Value);

        if (filter.CategoryId.HasValue)
            query = query.Where(r => r.RoomCategoryId == filter.CategoryId.Value);

        query = query.Where(r => r.IsAvailable == filter.IsAvailable);

        return query;
    }
}