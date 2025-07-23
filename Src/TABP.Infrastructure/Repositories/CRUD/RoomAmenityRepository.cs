using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.Infrastructure.contexts;
using TABP.Infrastructure.QueryExtensions;
using TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

namespace TABP.Infrastructure.Repositories.CRUD;

public class RoomAmenityRepository : IRoomAmenityRepository
{
    private readonly AppDbContext _context;

    public RoomAmenityRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RoomAmenity?> GetByIdAsync(int id)
    {
        return await _context.RoomAmenities
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<RoomAmenity>> GetAllAsync(RoomAmenityFilter filter)
    {
        var query = _context.RoomAmenities.AsQueryable();

        query = query.ApplyFilter(filter)
            .ApplySorting(filter.Sort)
            .ApplyPagination(filter.Paging);

        return await query.ToListAsync();
    }

    public async Task<RoomAmenity> AddAsync(RoomAmenity roomAmenity)
    {
        await _context.RoomAmenities.AddAsync(roomAmenity);
        await _context.SaveChangesAsync();
        return roomAmenity;
    }

    public async Task<bool> UpdateAsync(RoomAmenity roomAmenity)
    {
        var exists = await _context.RoomAmenities.AnyAsync(b => b.Id == roomAmenity.Id);
        if (exists is false)
            return false;

        _context.RoomAmenities.Update(roomAmenity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.RoomAmenities.FindAsync(id);
        if (entity is null)
            return false;

        _context.RoomAmenities.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
