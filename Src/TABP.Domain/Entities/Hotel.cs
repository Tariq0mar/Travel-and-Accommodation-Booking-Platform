namespace TABP.Domain.Entities;

public class Hotel
{
    public Guid ID { get; set; } = Guid.NewGuid();
    public Guid LocationID { get; set; }

    public required string Name { get; set; }
    public string? Description { get; set; }
    public byte StarRating { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    public required Location Location { get; set; }
    public ICollection<Room> Rooms { get; set; } = new List<Room>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<HotelGallery> Galleries { get; set; } = new List<HotelGallery>();
    public ICollection<HotelAmenities> Amenities { get; set; } = new List<HotelAmenities>();
    public ICollection<HotelDiscount> Discounts { get; set; } = new List<HotelDiscount>();
}