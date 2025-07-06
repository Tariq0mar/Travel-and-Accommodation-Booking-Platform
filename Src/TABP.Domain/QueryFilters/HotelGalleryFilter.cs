namespace TABP.Domain.QueryFilters;

public class HotelGalleryFilter
{
    public Guid? GalleryId { get; set; }
    public Guid? HotelId { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public List<SortCriteria> SortBy { get; set; } = new();
}