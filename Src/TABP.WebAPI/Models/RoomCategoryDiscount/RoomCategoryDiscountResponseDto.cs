namespace TABP.WebAPI.Models.RoomCategoryDiscount;

public class RoomCategoryDiscountResponseDto
{
    public int Id { get; set; }
    public int DiscountId { get; set; }
    public int RoomCategoryId { get; set; }

    public string DiscountName { get; set; } = string.Empty;
    public string RoomCategoryName { get; set; } = string.Empty;
}