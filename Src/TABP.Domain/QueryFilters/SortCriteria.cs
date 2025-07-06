namespace TABP.Domain.QueryFilters;

public class SortCriteria
{
    public required string Field { get; set; }
    public bool Descending { get; set; } = false;
}