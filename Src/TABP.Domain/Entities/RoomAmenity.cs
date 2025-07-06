namespace TABP.Domain.Entities;

public class RoomAmenity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid AmenityId { get; set; }
    public Guid RoomId { get; set; }

    public required Amenity Amenity { get; set; }
    public required Room Room { get; set; }
}