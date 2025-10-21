using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

public static class PaymentFilterExtension
{
    public static IQueryable<Payment> ApplyFilter(this IQueryable<Payment> query, PaymentFilter? filter)
    {
        if (filter is null)
            return query;

        if (filter.BookingId.HasValue)
            query = query.Where(p => p.BookingId == filter.BookingId.Value);

        if (filter.PaymentMethodId.HasValue)
            query = query.Where(p => p.PaymentMethod == filter.PaymentMethodId.Value);

        if (filter.MinAmount.HasValue)
            query = query.Where(p => p.Amount >= filter.MinAmount.Value);

        if (filter.MaxAmount.HasValue)
            query = query.Where(p => p.Amount <= filter.MaxAmount.Value);

        if (filter.Currency.HasValue)
            query = query.Where(p => p.Currency == filter.Currency.Value);

        if (filter.Status.HasValue)
            query = query.Where(p => p.PaymentStatus == filter.Status.Value);

        if (filter.CreatedAtFrom.HasValue)
            query = query.Where(p => p.CreatedAt >= filter.CreatedAtFrom.Value);

        if (filter.CreatedAtTo.HasValue)
            query = query.Where(p => p.CreatedAt <= filter.CreatedAtTo.Value);

        return query;
    }
}