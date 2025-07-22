using TABP.Domain.Enums;

namespace TABP.WebAPI.Models.Payment;

public class PaymentRequestDto
{
    public int BookingId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}