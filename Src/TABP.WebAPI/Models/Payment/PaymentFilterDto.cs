using TABP.Domain.Enums;

namespace TABP.WebAPI.Models.Payment;

public class PaymentFilterDto
{
    public int? BookingId { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public Currency? Currency { get; set; }
    public PaymentStatus? PaymentStatus { get; set; }

    public DateTime? CreatedAtFrom { get; set; }
    public DateTime? CreatedAtTo { get; set; }

    public string? Sort { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}