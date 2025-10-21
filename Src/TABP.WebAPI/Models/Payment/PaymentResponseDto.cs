using TABP.Domain.Enums;

namespace TABP.WebAPI.Models.Payment;

public class PaymentResponseDto
{
    public int Id { get; set; }
    public int BookingId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public DateTime CreatedAt { get; set; }
}