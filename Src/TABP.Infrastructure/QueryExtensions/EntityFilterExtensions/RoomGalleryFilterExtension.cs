using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

public static class RoomGalleryFilterExtension
{
    public static IQueryable<RoomGallery> ApplyFilter(this IQueryable<RoomGallery> query, RoomGalleryFilter? filter)
    {
        if (filter is null)
            return query;

        if (filter.GalleryId.HasValue)
            query = query.Where(rg => rg.GalleryId == filter.GalleryId.Value);

        if (filter.RoomId.HasValue)
            query = query.Where(rg => rg.RoomId == filter.RoomId.Value);

        return query;
    }
}