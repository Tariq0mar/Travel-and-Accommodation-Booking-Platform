namespace TABP.Domain.Entities;

public class Payment
{
    public Guid ID { get; set; } = Guid.NewGuid();
    public Guid BookingID { get; set; }

    public byte PaymentMethodID { get; set; }
    public decimal Amount { get; set; }
    public required string Currency { get; set; }
    public required string Status { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

    public required Booking Booking { get; set; }
}