using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

public static class CartItemFilterExtension
{
    public static IQueryable<CartItem> ApplyFilter(this IQueryable<CartItem> query, CartItemFilter? filter)
    {
        if (filter is null)
            return query;

        if (filter.UserId.HasValue)
            query = query.Where(ci => ci.UserId == filter.UserId.Value);

        if (filter.RoomId.HasValue)
            query = query.Where(ci => ci.RoomId == filter.RoomId.Value);

        if (filter.MinQuantity.HasValue)
            query = query.Where(ci => ci.Quantity >= filter.MinQuantity.Value);

        if (filter.MaxQuantity.HasValue)
            query = query.Where(ci => ci.Quantity <= filter.MaxQuantity.Value);

        if (filter.CreatedAtFrom.HasValue)
            query = query.Where(ci => ci.CreatedAt >= filter.CreatedAtFrom.Value);

        if (filter.CreatedAtTo.HasValue)
            query = query.Where(ci => ci.CreatedAt <= filter.CreatedAtTo.Value);

        if (filter.BookingConfirmed.HasValue)
            query = query.Where(ci => ci.BookingConfirmed == filter.BookingConfirmed.Value);

        if (filter.PaymentCompleted.HasValue)
            query = query.Where(ci => ci.PaymentCompleted == filter.PaymentCompleted.Value);

        return query;
    }
}