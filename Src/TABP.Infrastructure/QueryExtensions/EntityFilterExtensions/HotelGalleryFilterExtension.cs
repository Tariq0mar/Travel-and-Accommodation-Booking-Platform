using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

public static class HotelGalleryFilterExtension
{
    public static IQueryable<HotelGallery> ApplyFilter(this IQueryable<HotelGallery> query, HotelGalleryFilter? filter)
    {
        if (filter is null)
            return query;

        if (filter.GalleryId.HasValue)
            query = query.Where(hg => hg.GalleryId == filter.GalleryId.Value);

        if (filter.HotelId.HasValue)
            query = query.Where(hg => hg.HotelId == filter.HotelId.Value);

        return query;
    }
}