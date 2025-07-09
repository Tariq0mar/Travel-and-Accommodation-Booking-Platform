using TABP.Domain.Enums;

namespace TABP.Domain.Entities;

public class Booking
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RoomId { get; set; }

    public required Currency Currency { get; set; }
    public required decimal PriceWithoutDiscount { get; set; }
    public required decimal PriceWithDiscount { get; set; }
    public required byte AdultsCount { get; set; }
    public required byte ChildrenCount { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public required User User { get; set; }
    public required Room Room { get; set; }
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}