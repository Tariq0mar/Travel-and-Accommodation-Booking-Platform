namespace TABP.Domain.Entities;

public class HotelAmenities
{
    public Guid ID { get; set; }
    public Guid AmenityID { get; set; }
    public Guid HotelID { get; set; }

    public required Amenity Amenities { get; set; }
    public required Hotel Hotel { get; set; }
}