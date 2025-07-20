using TABP.Domain.Enums;

namespace TABP.WebAPI.Models.User;

public class UserFilterDto
{
    public int? LocationId { get; set; }
    public UserRole? UserRole { get; set; }
    public int? MinAge { get; set; }
    public int? MaxAge { get; set; }
    public Gender? Gender { get; set; }

    public string? UserName { get; set; }
    public string? Email { get; set; }

    public string? Sort { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}