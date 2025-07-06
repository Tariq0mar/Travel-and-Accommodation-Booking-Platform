using TABP.Domain.Enums;

namespace TABP.Domain.Entities;

public class Discount
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string Name { get; set; }
    public string? Description { get; set; }
    public required DiscountType DiscountType { get; set; }
    public required Currency Currency { get; set; }
    public bool IsActive { get; set; } = true;
    public required decimal Value { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<RoomCategoryDiscount> RoomCategoryDiscounts { get; set; } = new List<RoomCategoryDiscount>();
    public ICollection<HotelDiscount> HotelDiscounts { get; set; } = new List<HotelDiscount>();
    public ICollection<UserDiscount> UserDiscounts { get; set; } = new List<UserDiscount>();
}
