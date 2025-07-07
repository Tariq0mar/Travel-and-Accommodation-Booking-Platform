namespace TABP.Domain.QueryFilters.EntitiesFilters;

public class RoomCategoryFilter
{
    public string? Category { get; set; }

    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    public byte? MinAdultsCapacity { get; set; }
    public byte? MaxAdultsCapacity { get; set; }

    public byte? MinChildrenCapacity { get; set; }
    public byte? MaxChildrenCapacity { get; set; }

    public string? Sort { get; set; }
    public PaginationRecord Paging { get; set; } = new();
}