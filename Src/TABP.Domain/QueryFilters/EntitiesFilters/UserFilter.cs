using TABP.Domain.Enums;

namespace TABP.Domain.QueryFilters.EntitiesFilters;

public class UserFilter
{
    public int? LocationId { get; set; }

    public string? UserName { get; set; }
    public string? Email { get; set; }
    public UserRole? Role { get; set; }
    
    public int? MinAge { get; set; }
    public int? MaxAge { get; set; }

    public Gender? Gender { get; set; }
    public string? PhoneNumber { get; set; }

    public DateTime? CreationDateFrom { get; set; }
    public DateTime? CreationDateTo { get; set; }

    public string? Sort { get; set; }
    public PaginationRecord Paging { get; set; } = new();
}