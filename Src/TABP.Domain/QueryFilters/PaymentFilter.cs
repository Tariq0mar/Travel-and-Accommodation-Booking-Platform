using TABP.Domain.Enums;

namespace TABP.Domain.QueryFilters;

public class PaymentFilter
{
    public Guid? BookingId { get; set; }
    public PaymentMethod? PaymentMethodId { get; set; }
    
    public decimal? MinAmount { get; set; }
    public decimal? MaxAmount { get; set; }

    public Currency? Currency { get; set; }
    public PaymentStatus? Status { get; set; }

    public DateTime? CreatedAtFrom { get; set; }
    public DateTime? CreatedAtTo { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public List<SortCriteria> SortBy { get; set; } = new();
}