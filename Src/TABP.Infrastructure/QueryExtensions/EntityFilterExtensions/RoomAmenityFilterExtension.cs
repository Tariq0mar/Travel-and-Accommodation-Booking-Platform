using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

public static class RoomAmenityFilterExtension
{
    public static IQueryable<RoomAmenity> ApplyFilter(this IQueryable<RoomAmenity> query, RoomAmenityFilter? filter)
    {
        if (filter is null)
            return query;

        if (filter.AmenityId.HasValue)
            query = query.Where(ra => ra.AmenityId == filter.AmenityId.Value);

        if (filter.RoomId.HasValue)
            query = query.Where(ra => ra.RoomId == filter.RoomId.Value);

        return query;
    }
}