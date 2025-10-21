using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

public static class HotelDiscountFilterExtension
{
    public static IQueryable<HotelDiscount> ApplyFilter(this IQueryable<HotelDiscount> query, HotelDiscountFilter? filter)
    {
        if (filter is null)
            return query;

        if (filter.DiscountId.HasValue)
            query = query.Where(hd => hd.DiscountId == filter.DiscountId.Value);

        if (filter.HotelId.HasValue)
            query = query.Where(hd => hd.HotelId == filter.HotelId.Value);

        return query;
    }
}