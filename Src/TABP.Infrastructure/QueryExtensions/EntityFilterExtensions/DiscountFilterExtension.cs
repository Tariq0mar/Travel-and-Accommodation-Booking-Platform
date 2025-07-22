using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

public static class DiscountFilterExtension
{
    public static IQueryable<Discount> ApplyFilter(this IQueryable<Discount> query, DiscountFilter? filter)
    {
        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(d => d.Name.Contains(filter.Name));

        if (filter.DiscountType.HasValue)
            query = query.Where(d => d.DiscountType == filter.DiscountType.Value);

        if (filter.Currency.HasValue)
            query = query.Where(d => d.Currency == filter.Currency.Value);

        if (filter.IsActive.HasValue)
            query = query.Where(d => d.IsActive == filter.IsActive.Value);

        if (filter.MinValue.HasValue)
            query = query.Where(d => d.Value >= filter.MinValue.Value);

        if (filter.MaxValue.HasValue)
            query = query.Where(d => d.Value <= filter.MaxValue.Value);

        if (filter.StartDateFrom.HasValue)
            query = query.Where(d => d.StartDate >= filter.StartDateFrom.Value);

        if (filter.StartDateTo.HasValue)
            query = query.Where(d => d.StartDate <= filter.StartDateTo.Value);

        if (filter.EndDateFrom.HasValue)
            query = query.Where(d => d.EndDate >= filter.EndDateFrom.Value);

        if (filter.EndDateTo.HasValue)
            query = query.Where(d => d.EndDate <= filter.EndDateTo.Value);

        if (filter.CreatedAtFrom.HasValue)
            query = query.Where(d => d.CreatedAt >= filter.CreatedAtFrom.Value);

        if (filter.CreatedAtTo.HasValue)
            query = query.Where(d => d.CreatedAt <= filter.CreatedAtTo.Value);

        return query;
    }
}