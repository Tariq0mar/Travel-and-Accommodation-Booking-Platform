namespace TABP.Domain.Entities;

public class RoomCategory
{
    public Guid ID { get; set; } = Guid.NewGuid();

    public required string Category { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public byte AdultsCapacity { get; set; }
    public byte ChildrenCapacity { get; set; }

    public ICollection<Room> Rooms { get; set; } = new List<Room>();
    public ICollection<RoomCategoryDiscount> Discounts { get; set; } = new List<RoomCategoryDiscount>();
}