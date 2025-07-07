namespace TABP.Domain.QueryFilters.EntitiesFilters;

public class LocationFilter
{
    public string? Country { get; set; }
    public string? City { get; set; }

    public string? Sort { get; set; }
    public PaginationRecord Paging { get; set; } = new();
}