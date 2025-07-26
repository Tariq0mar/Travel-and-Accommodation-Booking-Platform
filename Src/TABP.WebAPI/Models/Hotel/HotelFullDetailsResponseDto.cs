using TABP.WebAPI.Models.Amenity;
using TABP.WebAPI.Models.Discount;
using TABP.WebAPI.Models.Gallery;
using TABP.WebAPI.Models.Location;
using TABP.WebAPI.Models.Review;
using TABP.WebAPI.Models.Room;

namespace TABP.WebAPI.Models.Hotel;

public class HotelFullDetailsResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public LocationResponseDto Location { get; set; }
    public List<GalleryResponseDto> Galleries { get; set; }
    public List<RoomResponseDto> Rooms { get; set; }
    public List<AmenityResponseDto> Amenities { get; set; }
    public List<DiscountResponseDto> Discounts { get; set; }
    public List<ReviewResponseDto> Reviews { get; set; }
}
