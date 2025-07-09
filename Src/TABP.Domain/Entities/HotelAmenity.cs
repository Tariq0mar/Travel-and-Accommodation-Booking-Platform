namespace TABP.Domain.Entities;

public class HotelAmenity
{
    public int Id { get; set; }
    public int AmenityId { get; set; }
    public int HotelId { get; set; }

    public required Amenity Amenity { get; set; }
    public required Hotel Hotel { get; set; }
}