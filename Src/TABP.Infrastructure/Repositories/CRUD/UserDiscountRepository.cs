using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.Infrastructure.contexts;
using TABP.Infrastructure.QueryExtensions;
using TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

namespace TABP.Infrastructure.Repositories.CRUD;

public class UserDiscountRepository : IUserDiscountRepository
{
    private readonly AppDbContext _context;

    public UserDiscountRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserDiscount?> GetByIdAsync(int id)
    {
        return await _context.UserDiscounts
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<UserDiscount>> GetAllAsync(UserDiscountFilter filter)
    {
        var query = _context.UserDiscounts.AsQueryable();

        query = query.ApplyFilter(filter)
            .ApplySorting(filter.Sort)
            .ApplyPagination(filter.Paging);

        return await query.ToListAsync();
    }

    public async Task<UserDiscount> AddAsync(UserDiscount userDiscount)
    {
        await _context.UserDiscounts.AddAsync(userDiscount);
        await _context.SaveChangesAsync();
        return userDiscount;
    }

    public async Task<bool> UpdateAsync(UserDiscount userDiscount)
    {
        var exists = await _context.UserDiscounts.AnyAsync(b => b.Id == userDiscount.Id);
        if (exists is false)
            return false;

        _context.UserDiscounts.Update(userDiscount);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.UserDiscounts.FindAsync(id);
        if (entity is null)
            return false;

        _context.UserDiscounts.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
