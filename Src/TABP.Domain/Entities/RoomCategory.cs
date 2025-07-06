using TABP.Domain.Enums;

namespace TABP.Domain.Entities;

public class RoomCategory
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string Category { get; set; }
    public string? Description { get; set; }
    public required Currency Currency {get; set;}
    public required decimal Price { get; set; }
    public required byte AdultsCapacity { get; set; }
    public required byte ChildrenCapacity { get; set; }

    public ICollection<Room> Rooms { get; set; } = new List<Room>();
    public ICollection<RoomCategoryDiscount> RoomCategoryDiscounts { get; set; } = new List<RoomCategoryDiscount>();
}