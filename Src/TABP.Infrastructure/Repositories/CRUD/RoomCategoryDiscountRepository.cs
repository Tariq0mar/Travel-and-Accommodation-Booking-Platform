using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.Infrastructure.contexts;
using TABP.Infrastructure.QueryExtensions;
using TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

namespace TABP.Infrastructure.Repositories.CRUD;

public class RoomCategoryDiscountRepository : IRoomCategoryDiscountRepository
{
    private readonly AppDbContext _context;

    public RoomCategoryDiscountRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RoomCategoryDiscount?> GetByIdAsync(int id)
    {
        return await _context.RoomCategoryDiscounts
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<RoomCategoryDiscount>> GetAllAsync(RoomCategoryDiscountFilter filter)
    {
        var query = _context.RoomCategoryDiscounts.AsQueryable();

        query = query.ApplyFilter(filter)
            .ApplySorting(filter.Sort)
            .ApplyPagination(filter.Paging);

        return await query.ToListAsync();
    }

    public async Task<RoomCategoryDiscount> AddAsync(RoomCategoryDiscount roomCategoryDiscount)
    {
        await _context.RoomCategoryDiscounts.AddAsync(roomCategoryDiscount);
        await _context.SaveChangesAsync();
        return roomCategoryDiscount;
    }

    public async Task<bool> UpdateAsync(RoomCategoryDiscount roomCategoryDiscount)
    {
        var exists = await _context.RoomCategoryDiscounts.AnyAsync(b => b.Id == roomCategoryDiscount.Id);
        if (exists is false)
            return false;

        _context.RoomCategoryDiscounts.Update(roomCategoryDiscount);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.RoomCategoryDiscounts.FindAsync(id);
        if (entity is null)
            return false;

        _context.RoomCategoryDiscounts.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
