namespace TABP.WebAPI.Models.Location;

public class LocationResponseDto
{
    public int Id { get; set; }
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string? Description { get; set; }
}