namespace TABP.Domain.Entities;

public class Cart
{
    public Guid ID { get; set; } = Guid.NewGuid();
    public Guid UserID { get; set; }
    public Guid RoomID { get; set; }

    public byte Quantity { get; set; }
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;

    public required User User { get; set; }
    public required Room Room { get; set; }
}