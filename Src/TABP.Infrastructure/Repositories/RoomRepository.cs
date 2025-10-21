using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.Infrastructure.contexts;
using TABP.Infrastructure.QueryExtensions;
using TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

namespace TABP.Infrastructure.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly AppDbContext _context;

    public RoomRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Room?> GetByIdAsync(int id)
    {
        return await _context.Rooms
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Room?> GetByIdFullDetailsAsync(int id)
    {
        return await _context.Rooms
                .Include(r => r.RoomCategory)
                    .ThenInclude(c => c.RoomCategoryDiscounts)
                        .ThenInclude(rc => rc.Discount)
                .Include(r => r.Hotel)
                .Include(r => r.RoomGalleries)
                .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Room>> GetAllAsync(RoomFilter filter)
    {
        var query = _context.Rooms.AsQueryable();

        query = query.ApplyFilter(filter)
            .ApplySorting(filter.Sort)
            .ApplyPagination(filter.Paging);

        return await query.ToListAsync();
    }

    public async Task<Room> AddAsync(Room room)
    {
        await _context.Rooms.AddAsync(room);
        await _context.SaveChangesAsync();
        return room;
    }

    public async Task<bool> UpdateAsync(Room room)
    {
        var exists = await _context.Rooms.AnyAsync(b => b.Id == room.Id);
        if (exists is false)
            return false;

        _context.Rooms.Update(room);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Rooms.FindAsync(id);
        if (entity is null)
            return false;

        _context.Rooms.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}