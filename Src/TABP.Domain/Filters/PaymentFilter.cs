namespace TABP.Domain.Filters;

public class PaymentFilter
{
    public Guid? BookingID { get; set; }

    public byte? PaymentMethodID { get; set; }
    
    public decimal? MinAmount { get; set; }
    public decimal? MaxAmount { get; set; }

    public string? Currency { get; set; }
    public string? Status { get; set; }

    public DateTime? PaymentDateFrom { get; set; }
    public DateTime? PaymentDateTo { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public List<SortCriteria> SortBy { get; set; } = new List<SortCriteria>();
}