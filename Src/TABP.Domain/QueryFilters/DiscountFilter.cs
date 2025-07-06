using TABP.Domain.Enums;

namespace TABP.Domain.QueryFilters;

public class DiscountFilter
{
    public string? Name { get; set; }
    public DiscountType? DiscountType { get; set; }
    public Currency? Currency { get; set; }
    public bool? IsActive { get; set; }

    public decimal? MinValue { get; set; }
    public decimal? MaxValue { get; set; }

    public DateTime? StartDateFrom { get; set; }
    public DateTime? StartDateTo { get; set; }

    public DateTime? EndDateFrom { get; set; }
    public DateTime? EndDateTo { get; set; }

    public DateTime? CreatedAtFrom { get; set; }
    public DateTime? CreatedAtTo { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public List<SortCriteria> SortBy { get; set; } = new();
}