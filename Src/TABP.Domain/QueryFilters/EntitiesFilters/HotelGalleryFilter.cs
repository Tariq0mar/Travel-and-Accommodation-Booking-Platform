namespace TABP.Domain.QueryFilters.EntitiesFilters;

public class HotelGalleryFilter
{
    public Guid? GalleryId { get; set; }
    public Guid? HotelId { get; set; }

    public string? Sort { get; set; }
    public PaginationRecord Paging { get; set; } = new();
}