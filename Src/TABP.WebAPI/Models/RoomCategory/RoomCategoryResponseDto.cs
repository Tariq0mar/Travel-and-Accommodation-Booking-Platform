using TABP.Domain.Enums;

namespace TABP.WebAPI.Models.RoomCategory;

public class RoomCategoryResponseDto
{
    public int Id { get; set; }
    public RoomCategoryType Category { get; set; }
    public string? Description { get; set; }
    public Currency Currency { get; set; }
    public decimal Price { get; set; }
    public byte AdultsCount { get; set; }
    public byte ChildrenCount { get; set; }
}