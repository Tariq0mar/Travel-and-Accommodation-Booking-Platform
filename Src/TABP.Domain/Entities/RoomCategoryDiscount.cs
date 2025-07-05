namespace TABP.Domain.Entities;

public class RoomCategoryDiscount
{
    public Guid ID { get; set; }
    public Guid DiscountID { get; set; }
    public Guid RoomCategoryID { get; set; }

    public required Discount Discount { get; set; }
    public required RoomCategory RoomCategory { get; set; }
}