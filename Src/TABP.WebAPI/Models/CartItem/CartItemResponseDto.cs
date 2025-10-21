namespace TABP.WebAPI.Models.CartItem;

public class CartItemResponseDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RoomId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool BookingConfirmed { get; set; }
    public bool PaymentCompleted { get; set; }
}