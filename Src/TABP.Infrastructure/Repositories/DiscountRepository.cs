using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.Infrastructure.contexts;
using TABP.Infrastructure.QueryExtensions;
using TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

namespace TABP.Infrastructure.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly AppDbContext _context;

    public DiscountRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Discount?> GetByIdAsync(int id)
    {
        return await _context.Discounts
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Discount>> GetAllAsync(DiscountFilter filter)
    {
        var query = _context.Discounts.AsQueryable();

        query = query.ApplyFilter(filter)
            .ApplySorting(filter.Sort)
            .ApplyPagination(filter.Paging);

        return await query.ToListAsync();
    }

    public async Task<Discount> AddAsync(Discount discount)
    {
        await _context.Discounts.AddAsync(discount);
        await _context.SaveChangesAsync();
        return discount;
    }

    public async Task<bool> UpdateAsync(Discount discount)
    {
        var exists = await _context.Discounts.AnyAsync(b => b.Id == discount.Id);
        if (exists is false)
            return false;

        _context.Discounts.Update(discount);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Discounts.FindAsync(id);
        if (entity is null)
            return false;

        _context.Discounts.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}