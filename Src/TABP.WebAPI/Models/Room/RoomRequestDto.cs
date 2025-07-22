namespace TABP.WebAPI.Models.Room;

public class RoomRequestDto
{
    public int HotelId { get; set; }
    public int RoomCategoryId { get; set; }
    public string Number { get; set; } = string.Empty;
    public bool IsAvailable { get; set; } = true;
}