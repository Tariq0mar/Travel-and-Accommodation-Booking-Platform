namespace TABP.Domain.Entities;

public class CartItem
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RoomId { get; set; }

    public int Quantity { get; set; } = 1;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool BookingConfirmed { get; set; } = false;
    public bool PaymentCompleted { get; set; } = false;

    public required User User { get; set; }
    public required Room Room { get; set; }
}