namespace TABP.Domain.Entities;

public class Discount
{
    public Guid ID { get; set; } = Guid.NewGuid();

    public required string Name { get; set; }
    public string? Description { get; set; }
    public required byte DiscountType { get; set; }
    public required decimal Value { get; set; }
    public required byte Currency { get; set; }
    public bool IsActive { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<RoomCategoryDiscount> TargetRoomCategories { get; set; } = new List<RoomCategoryDiscount>();
    public ICollection<HotelDiscount> TargetHotels { get; set; } = new List<HotelDiscount>();
    public ICollection<UserDiscount> TargetUsers { get; set; } = new List<UserDiscount>();
}
