namespace TABP.WebAPI.Models.Location;

public class LocationRequestDto
{
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string? Description { get; set; }
}