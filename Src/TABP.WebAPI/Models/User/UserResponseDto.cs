using TABP.Domain.Enums;

namespace TABP.WebAPI.Models.User;

public class UserResponseDto
{
    public int Id { get; set; }
    public int LocationId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRole UserRole { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}