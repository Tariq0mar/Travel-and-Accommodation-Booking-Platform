using TABP.Domain.Enums;

namespace TABP.Domain.Models.BookingRoom;

public class BookingRoomModel
{
    public int UserId { get; set; }
    public int RoomId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public byte AdultsCount { get; set; }
    public byte ChildrenCount { get; set; }
    public Currency Currency { get; set; }
    public decimal PriceWithoutDiscount { get; set; }
    public decimal PriceWithDiscount { get; set; }
}