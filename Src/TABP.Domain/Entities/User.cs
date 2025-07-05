namespace TABP.Domain.Entities;

public class User
{
    public Guid ID { get; set; } = Guid.NewGuid();
    public Guid LocationID { get; set; }

    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string HashedPassword { get; set; }
    public required byte Role { get; set; }
    public required byte Age { get; set; }
    public required string Gender { get; set; }
    public required string PhoneNumber { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    public required Location Location { get; set; }
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<Cart> CartItems { get; set; } = new List<Cart>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<UserDiscount> Discounts { get; set; } = new List<UserDiscount>();
}