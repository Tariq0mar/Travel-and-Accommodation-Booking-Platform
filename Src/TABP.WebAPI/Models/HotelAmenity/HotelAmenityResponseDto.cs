namespace TABP.WebAPI.Models.HotelAmenity;

public class HotelAmenityResponseDto
{
    public int Id { get; set; }
    public int AmenityId { get; set; }
    public int HotelId { get; set; }

    public string AmenityName { get; set; } = string.Empty;
    public string HotelName { get; set; } = string.Empty;
}