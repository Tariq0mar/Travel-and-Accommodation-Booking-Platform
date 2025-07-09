namespace TABP.Domain.Entities;

public class RoomCategoryDiscount
{
    public int Id { get; set; }
    public int DiscountId { get; set; }
    public int RoomCategoryId { get; set; }

    public required Discount Discount { get; set; }
    public required RoomCategory RoomCategory { get; set; }
}