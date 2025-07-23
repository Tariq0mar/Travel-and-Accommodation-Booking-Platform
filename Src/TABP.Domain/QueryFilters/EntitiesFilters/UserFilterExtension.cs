using TABP.Domain.Entities;

namespace TABP.Domain.QueryFilters.EntitiesFilters;

public static class UserFilterExtension
{
    public static IQueryable<User> ApplyFilter(this IQueryable<User> query, UserFilter? filter)
    {
        if (filter is null)
            return query;

        if (filter.LocationId.HasValue)
            query = query.Where(u => u.LocationId == filter.LocationId.Value);

        if (!string.IsNullOrWhiteSpace(filter.UserName))
            query = query.Where(u => u.UserName.Contains(filter.UserName));

        if (!string.IsNullOrWhiteSpace(filter.Email))
            query = query.Where(u => u.Email.Contains(filter.Email));

        if (filter.Role.HasValue)
            query = query.Where(u => u.UserRole == filter.Role.Value);

        if (filter.MinAge.HasValue)
            query = query.Where(u => u.Age >= filter.MinAge.Value);

        if (filter.MaxAge.HasValue)
            query = query.Where(u => u.Age <= filter.MaxAge.Value);

        if (filter.Gender.HasValue)
            query = query.Where(u => u.Gender == filter.Gender.Value);

        if (!string.IsNullOrWhiteSpace(filter.PhoneNumber))
            query = query.Where(u => u.PhoneNumber.Contains(filter.PhoneNumber));

        if (filter.CreationDateFrom.HasValue)
            query = query.Where(u => u.CreatedAt >= filter.CreationDateFrom.Value);

        if (filter.CreationDateTo.HasValue)
            query = query.Where(u => u.CreatedAt <= filter.CreationDateTo.Value);

        return query;
    }
}