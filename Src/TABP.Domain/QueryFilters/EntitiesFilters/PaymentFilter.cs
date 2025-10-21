using TABP.Domain.Enums;

namespace TABP.Domain.QueryFilters.EntitiesFilters;

public class PaymentFilter
{
    public int? BookingId { get; set; }
    public PaymentMethod? PaymentMethodId { get; set; }
    
    public decimal? MinAmount { get; set; }
    public decimal? MaxAmount { get; set; }

    public Currency? Currency { get; set; }
    public PaymentStatus? Status { get; set; }

    public DateTime? CreatedAtFrom { get; set; }
    public DateTime? CreatedAtTo { get; set; }

    public string? Sort { get; set; }
    public PaginationRecord Paging { get; set; } = new();
}