using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

public static class UserDiscountFilterExtension
{
    public static IQueryable<UserDiscount> ApplyFilter(this IQueryable<UserDiscount> query, UserDiscountFilter? filter)
    {
        if (filter is null)
            return query;

        if (filter.DiscountId.HasValue)
            query = query.Where(ud => ud.DiscountId == filter.DiscountId.Value);

        if (filter.UserId.HasValue)
            query = query.Where(ud => ud.UserId == filter.UserId.Value);

        return query;
    }
}