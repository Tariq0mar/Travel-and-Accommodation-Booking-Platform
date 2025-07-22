namespace TABP.WebAPI.Models.Room;

public class RoomResponseDto
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public int RoomCategoryId { get; set; }
    public string Number { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
}