namespace TABP.Domain.Entities;

public class Gallery
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string ImageUrl { get; set; }
    public string? Caption { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}