namespace TABP.Domain.Entities;

public class Gallery
{
    public int Id { get; set; }

    public required string ImageUrl { get; set; }
    public string? Caption { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}