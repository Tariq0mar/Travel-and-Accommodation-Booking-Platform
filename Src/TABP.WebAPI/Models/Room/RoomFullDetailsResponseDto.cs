using TABP.WebAPI.Models.Discount;
using TABP.WebAPI.Models.Hotel;
using TABP.WebAPI.Models.RoomCategory;

namespace TABP.WebAPI.Models.Room;

public class RoomFullDetailsResponseDto
{
    public int Id { get; set; }
    public string RoomNumber { get; set; } = default!;
    public decimal PricePerNight { get; set; }
    public int Capacity { get; set; }
    public bool IsAvailable { get; set; }

    public RoomCategoryResponseDto Category { get; set; } = default!;
    public HotelResponseDto Hotel { get; set; } = default!;
    public List<string> Images { get; set; } = new();
    public DiscountResponseDto? Discount { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
