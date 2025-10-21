using TABP.Domain.Enums;

namespace TABP.WebAPI.Models.Booking;

public class BookingRequestDto
{
    public int UserId { get; set; }
    public int RoomId { get; set; }
    public Currency Currency { get; set; }
    public decimal PriceWithoutDiscount { get; set; }
    public decimal PriceWithDiscount { get; set; }
    public byte AdultsCount { get; set; }
    public byte ChildrenCount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreatedAt { get; set; }
}