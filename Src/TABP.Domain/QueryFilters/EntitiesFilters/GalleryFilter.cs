namespace TABP.Domain.QueryFilters.EntitiesFilters;

public class GalleryFilter
{
    public DateTime? CreatedAtFrom { get; set; }
    public DateTime? CreatedAtTo { get; set; }

    public string? Sort { get; set; }
    public PaginationRecord Paging { get; set; } = new();
}