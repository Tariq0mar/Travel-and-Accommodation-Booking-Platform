using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

public static class BookingFilterExtension
{
    public static IQueryable<Booking> ApplyFilter(this IQueryable<Booking> query, BookingFilter? filter)
    {
        if (filter is null)
            return query;

        if (filter.UserId.HasValue)
            query = query.Where(b => b.UserId == filter.UserId.Value);

        if (filter.RoomId.HasValue)
            query = query.Where(b => b.RoomId == filter.RoomId.Value);

        if (filter.Currency.HasValue)
            query = query.Where(b => b.Currency == filter.Currency.Value);

        if (filter.MinTotalPrice.HasValue)
            query = query.Where(b => b.PriceWithDiscount >= filter.MinTotalPrice.Value);

        if (filter.MaxTotalPrice.HasValue)
            query = query.Where(b => b.PriceWithDiscount <= filter.MaxTotalPrice.Value);

        if (filter.MinAdultsCount.HasValue)
            query = query.Where(b => b.AdultsCount >= filter.MinAdultsCount.Value);

        if (filter.MaxAdultsCount.HasValue)
            query = query.Where(b => b.AdultsCount <= filter.MaxAdultsCount.Value);

        if (filter.MinChildrenCount.HasValue)
            query = query.Where(b => b.ChildrenCount >= filter.MinChildrenCount.Value);

        if (filter.MaxChildrenCount.HasValue)
            query = query.Where(b => b.ChildrenCount <= filter.MaxChildrenCount.Value);

        if (filter.StartDateFrom.HasValue)
            query = query.Where(b => b.StartDate >= filter.StartDateFrom.Value);

        if (filter.StartDateTo.HasValue)
            query = query.Where(b => b.StartDate <= filter.StartDateTo.Value);

        if (filter.CreatedAtFrom.HasValue)
            query = query.Where(b => b.CreatedAt >= filter.CreatedAtFrom.Value);

        if (filter.CreatedAtTo.HasValue)
            query = query.Where(b => b.CreatedAt <= filter.CreatedAtTo.Value);

        if (filter.BookingDateFrom.HasValue)
            query = query.Where(b => b.StartDate >= filter.BookingDateFrom.Value); 

        if (filter.BookingDateTo.HasValue)
            query = query.Where(b => b.StartDate <= filter.BookingDateTo.Value);

        return query;
    }
}