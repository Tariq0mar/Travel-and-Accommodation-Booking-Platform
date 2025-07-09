namespace TABP.Domain.Entities;

public class Location
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string Country { get; set; }
    public required string City { get; set; }
    public string? Description { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
}