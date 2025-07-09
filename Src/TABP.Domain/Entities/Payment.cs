using TABP.Domain.Enums;

namespace TABP.Domain.Entities;

public class Payment
{
    public int Id { get; set; }
    public int BookingId { get; set; }

    public required PaymentMethod PaymentMethod { get; set; }
    public required decimal Amount { get; set; }
    public required Currency Currency { get; set; }
    public required PaymentStatus PaymentStatus { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public required Booking Booking { get; set; }
}