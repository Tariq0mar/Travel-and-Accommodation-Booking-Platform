namespace TABP.Domain.Entities;

public class RoomCategoryDiscount
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid DiscountId { get; set; }
    public Guid RoomCategoryId { get; set; }

    public required Discount Discount { get; set; }
    public required RoomCategory RoomCategory { get; set; }
}