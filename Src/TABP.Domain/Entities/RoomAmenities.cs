namespace TABP.Domain.Entities;

public class RoomAmenities
{
    public Guid ID { get; set; }
    public Guid AmenityID { get; set; }
    public Guid RoomID { get; set; }

    public required Amenity Amenities { get; set; }
    public required Room Room { get; set; }
}