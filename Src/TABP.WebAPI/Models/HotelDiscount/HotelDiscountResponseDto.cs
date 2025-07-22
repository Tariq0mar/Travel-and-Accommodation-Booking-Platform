namespace TABP.WebAPI.Models.HotelDiscount;

public class HotelDiscountResponseDto
{
    public int Id { get; set; }
    public int DiscountId { get; set; }
    public int HotelId { get; set; }

    public string DiscountName { get; set; } = string.Empty;
    public string HotelName { get; set; } = string.Empty;
}