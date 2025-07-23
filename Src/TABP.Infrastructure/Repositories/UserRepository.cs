using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.Infrastructure.contexts;
using TABP.Infrastructure.QueryExtensions;
using TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

namespace TABP.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<User>> GetAllAsync(UserFilter filter)
    {
        var query = _context.Users.AsQueryable();

        query = query.ApplyFilter(filter)
            .ApplySorting(filter.Sort)
            .ApplyPagination(filter.Paging);

        return await query.ToListAsync();
    }

    public async Task<User> AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> UpdateAsync(User user)
    {
        var exists = await _context.Users.AnyAsync(b => b.Id == user.Id);
        if (exists is false)
            return false;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Users.FindAsync(id);
        if (entity is null)
            return false;

        _context.Users.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}