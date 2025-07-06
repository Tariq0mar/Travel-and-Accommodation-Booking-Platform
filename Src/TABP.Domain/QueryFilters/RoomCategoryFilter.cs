namespace TABP.Domain.QueryFilters;

public class RoomCategoryFilter
{
    public string? Category { get; set; }

    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    public byte? MinAdultsCapacity { get; set; }
    public byte? MaxAdultsCapacity { get; set; }

    public byte? MinChildrenCapacity { get; set; }
    public byte? MaxChildrenCapacity { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public List<SortCriteria> SortBy { get; set; } = new();
}