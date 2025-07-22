using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.Infrastructure.contexts;
using TABP.Infrastructure.QueryExtensions;
using TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

namespace TABP.Infrastructure.Repositories;

public class RoomCategoryRepository : IRoomCategoryRepository
{
    private readonly AppDbContext _context;

    public RoomCategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RoomCategory?> GetByIdAsync(int id)
    {
        return await _context.RoomCategories
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<RoomCategory>> GetAllAsync(RoomCategoryFilter filter)
    {
        var query = _context.RoomCategories.AsQueryable();

        query = query.ApplyFilter(filter)
            .ApplySorting(filter.Sort)
            .ApplyPagination(filter.Paging);

        return await query.ToListAsync();
    }

    public async Task<RoomCategory> AddAsync(RoomCategory roomCategory)
    {
        await _context.RoomCategories.AddAsync(roomCategory);
        await _context.SaveChangesAsync();
        return roomCategory;
    }

    public async Task<bool> UpdateAsync(RoomCategory roomCategory)
    {
        var exists = await _context.RoomCategories.AnyAsync(b => b.Id == roomCategory.Id);
        if (exists is false)
            return false;

        _context.RoomCategories.Update(roomCategory);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.RoomCategories.FindAsync(id);
        if (entity is null)
            return false;

        _context.RoomCategories.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
