namespace TABP.Domain.Entities;

public class HotelAmenity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid AmenityId { get; set; }
    public Guid HotelId { get; set; }

    public required Amenity Amenity { get; set; }
    public required Hotel Hotel { get; set; }
}