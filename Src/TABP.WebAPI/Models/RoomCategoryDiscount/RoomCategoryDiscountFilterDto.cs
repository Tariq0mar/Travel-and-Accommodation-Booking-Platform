namespace TABP.WebAPI.Models.RoomCategoryDiscount;

public class RoomCategoryDiscountFilterDto
{
    public int? DiscountId { get; set; }
    public int? RoomCategoryId { get; set; }

    public string? Sort { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}