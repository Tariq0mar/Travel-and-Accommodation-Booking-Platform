namespace TABP.WebAPI.Models.RoomAmenity;

public class RoomAmenityResponseDto
{
    public int Id { get; set; }
    public int AmenityId { get; set; }
    public int RoomId { get; set; }

    public string AmenityName { get; set; } = string.Empty;
    public string RoomNumber { get; set; } = string.Empty;
}