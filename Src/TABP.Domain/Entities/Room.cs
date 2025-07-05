namespace TABP.Domain.Entities;

public class Room
{
    public Guid ID { get; set; } = Guid.NewGuid();
    public Guid HotelID { get; set; }
    public Guid CategoryID { get; set; }

    public required string Number { get; set; }
    public bool IsAvailable { get; set; }

    public required Hotel Hotel { get; set; }
    public required RoomCategory Category { get; set; }
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<Cart> CartItems { get; set; } = new List<Cart>();
    public ICollection<RoomGallery> Galleries { get; set; } = new List<RoomGallery>();
    public ICollection<RoomAmenities> Amenities { get; set; } = new List<RoomAmenities>();
}