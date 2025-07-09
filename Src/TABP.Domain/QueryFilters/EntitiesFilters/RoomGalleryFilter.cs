namespace TABP.Domain.QueryFilters.EntitiesFilters;

public class RoomGalleryFilter
{
    public Guid? GalleryId { get; set; }
    public Guid? RoomId { get; set; }

    public string? Sort { get; set; }
    public PaginationRecord Paging { get; set; } = new();
}