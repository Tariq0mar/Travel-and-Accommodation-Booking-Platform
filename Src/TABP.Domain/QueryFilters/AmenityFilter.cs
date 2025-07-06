namespace TABP.Domain.QueryFilters;

public class AmenityFilter
{
    public string? Name { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public List<SortCriteria> SortBy { get; set; } = new();
}