namespace TABP.Domain.Filters;

public class DiscountFilter
{
    public string? Name { get; set; }
    public byte? DiscountType { get; set; }
    public decimal? MinValue { get; set; }
    public decimal? MaxValue { get; set; }
    public byte? Currency { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime StartDateFrom { get; set; }
    public DateTime StartDateTo { get; set; }

    public DateTime EndDateFrom { get; set; }
    public DateTime EndDateTo { get; set; }

    public DateTime CreatedAtFrom { get; set; }
    public DateTime CreatedAtTo { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public List<SortCriteria> SortBy { get; set; } = new List<SortCriteria>();
}