namespace TABP.Domain.Filters;

public class SortCriteria
{
    public required string Field { get; set; }
    public bool Descending { get; set; } = false;

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public List<SortCriteria> SortBy { get; set; } = new List<SortCriteria>();
}