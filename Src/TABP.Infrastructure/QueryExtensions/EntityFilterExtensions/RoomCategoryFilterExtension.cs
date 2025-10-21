using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

public static class RoomCategoryFilterExtension
{
    public static IQueryable<RoomCategory> ApplyFilter(this IQueryable<RoomCategory> query, RoomCategoryFilter? filter)
    {
        if (filter is null)
            return query;

        if (filter.Category.HasValue)
            query = query.Where(rc => rc.Category == filter.Category.Value);

        if (filter.MinPrice.HasValue)
            query = query.Where(rc => rc.Price >= filter.MinPrice.Value);

        if (filter.MaxPrice.HasValue)
            query = query.Where(rc => rc.Price <= filter.MaxPrice.Value);

        if (filter.MinAdultsCount.HasValue)
            query = query.Where(rc => rc.AdultsCount >= filter.MinAdultsCount.Value);

        if (filter.MaxAdultsCount.HasValue)
            query = query.Where(rc => rc.AdultsCount <= filter.MaxAdultsCount.Value);

        if (filter.MinChildrenCount.HasValue)
            query = query.Where(rc => rc.ChildrenCount >= filter.MinChildrenCount.Value);

        if (filter.MaxChildrenCount.HasValue)
            query = query.Where(rc => rc.ChildrenCount <= filter.MaxChildrenCount.Value);

        return query;
    }
}