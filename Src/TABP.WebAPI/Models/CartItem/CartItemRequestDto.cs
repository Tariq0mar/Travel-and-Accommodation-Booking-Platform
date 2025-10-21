namespace TABP.WebAPI.Models.CartItem;

public class CartItemRequestDto
{
    public int UserId { get; set; }
    public int RoomId { get; set; }
    public int Quantity { get; set; } = 1;
}