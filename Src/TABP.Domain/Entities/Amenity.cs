namespace TABP.Domain.Entities;

public class Amenity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string Name { get; set; }
    public string? Description { get; set; }
}