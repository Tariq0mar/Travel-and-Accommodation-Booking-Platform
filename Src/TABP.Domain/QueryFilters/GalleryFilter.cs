namespace TABP.Domain.QueryFilters;

public class GalleryFilter
{
    public DateTime? CreatedAtFrom { get; set; }
    public DateTime? CreatedAtTo { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public List<SortCriteria> SortBy { get; set; } = new();
}