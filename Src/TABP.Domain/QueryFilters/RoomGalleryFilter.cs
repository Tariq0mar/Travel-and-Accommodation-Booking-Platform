namespace TABP.Domain.QueryFilters;

public class RoomGalleryFilter
{
    public Guid? GalleryId { get; set; }
    public Guid? RoomId { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public List<SortCriteria> SortBy { get; set; } = new List<SortCriteria>();
}