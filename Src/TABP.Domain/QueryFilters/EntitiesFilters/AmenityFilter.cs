namespace TABP.Domain.QueryFilters.EntitiesFilters;

public class AmenityFilter
{
    public string? Name { get; set; }

    public string? Sort { get; set; }
    public PaginationRecord Paging { get; set; } = new ();
}