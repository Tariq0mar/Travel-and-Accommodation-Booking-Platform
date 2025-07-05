namespace TABP.Domain.Entities;

public class Booking
{
    public Guid ID { get; set; } = Guid.NewGuid();
    public Guid UserID { get; set; }
    public Guid RoomID { get; set; }

    public required string Currency { get; set; }
    public decimal TotalPrice { get; set; }
    public byte AdultsCount { get; set; }
    public byte ChildrenCount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime BookingDate { get; set; }

    public required User User { get; set; }
    public required Room Room { get; set; }
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}