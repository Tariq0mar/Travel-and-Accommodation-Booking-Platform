namespace TABP.WebAPI.Models.Login;

public class LoginRequestDto
{
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
}