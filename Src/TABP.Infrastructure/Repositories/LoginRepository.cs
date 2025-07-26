using Microsoft.EntityFrameworkCore;
using TABP.Domain.BusinessServiceModels.Login;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
using TABP.Infrastructure.contexts;

namespace TABP.Infrastructure.Repositories;

public class LoginRepository : ILoginRepository
{
    private readonly AppDbContext _context;

    public LoginRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> LoginAsync(LoginModel loginModel)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(b => b.Email == loginModel.Email);

        if (user is null)
        {
            return null;
        }

        if (user.PasswordHash != loginModel.PasswordHash)
        {
            return null;
        }

        return user;
    }
}