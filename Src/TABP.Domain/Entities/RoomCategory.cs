using TABP.Domain.Enums;

namespace TABP.Domain.Entities;

public class RoomCategory
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required RoomCategoryType Category { get; set; }
    public string? Description { get; set; }
    public required Currency Currency {get; set;}
    public required decimal Price { get; set; }
    public required byte AdultsCount { get; set; }
    public required byte ChildrenCount { get; set; }

    public ICollection<Room> Rooms { get; set; } = new List<Room>();
    public ICollection<RoomCategoryDiscount> RoomCategoryDiscounts { get; set; } = new List<RoomCategoryDiscount>();
}