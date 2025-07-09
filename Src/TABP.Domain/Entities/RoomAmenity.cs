namespace TABP.Domain.Entities;

public class RoomAmenity
{
    public int Id { get; set; }
    public int AmenityId { get; set; }
    public int RoomId { get; set; }

    public required Amenity Amenity { get; set; }
    public required Room Room { get; set; }
}