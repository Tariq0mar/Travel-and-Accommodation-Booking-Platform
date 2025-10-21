using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

public static class RoomCategoryDiscountFilterExtension
{
    public static IQueryable<RoomCategoryDiscount> ApplyFilter(this IQueryable<RoomCategoryDiscount> query, RoomCategoryDiscountFilter? filter)
    {
        if (filter is null)
            return query;

        if (filter.DiscountId.HasValue)
            query = query.Where(rcd => rcd.DiscountId == filter.DiscountId.Value);

        if (filter.RoomCategoryId.HasValue)
            query = query.Where(rcd => rcd.RoomCategoryId == filter.RoomCategoryId.Value);

        return query;
    }
}