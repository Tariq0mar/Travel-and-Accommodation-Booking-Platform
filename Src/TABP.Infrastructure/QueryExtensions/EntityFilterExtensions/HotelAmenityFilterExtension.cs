using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

public static class HotelAmenityFilterExtension
{
    public static IQueryable<HotelAmenity> ApplyFilter(this IQueryable<HotelAmenity> query, HotelAmenityFilter? filter)
    {
        if (filter is null)
            return query;

        if (filter.AmenityId.HasValue)
            query = query.Where(ha => ha.AmenityId == filter.AmenityId.Value);

        if (filter.HotelId.HasValue)
            query = query.Where(ha => ha.HotelId == filter.HotelId.Value);

        return query;
    }
}