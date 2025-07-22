namespace TABP.WebAPI.Models.CartItem;

public class CartItemFilterDto
{
    public int? UserId { get; set; }
    public int? RoomId { get; set; }
    public int? MinQuantity { get; set; }
    public int? MaxQuantity { get; set; }

    public bool? BookingConfirmed { get; set; }
    public bool? PaymentCompleted { get; set; }

    public DateTime? CreatedAtFrom { get; set; }
    public DateTime? CreatedAtTo { get; set; }

    public string? Sort { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}