namespace TABP.Domain.Entities;

public class CartItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public Guid RoomId { get; set; }

    public int Quantity { get; set; } = 1;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool BookingConfirmed { get; set; } = false;
    public bool PaymentCompleted { get; set; } = false;

    public required User User { get; set; }
    public required Room Room { get; set; }
}