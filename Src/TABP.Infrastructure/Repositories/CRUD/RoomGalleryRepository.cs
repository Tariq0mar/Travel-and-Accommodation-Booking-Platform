using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.Infrastructure.contexts;
using TABP.Infrastructure.QueryExtensions;
using TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

namespace TABP.Infrastructure.Repositories.CRUD;

public class RoomGalleryRepository : IRoomGalleryRepository
{
    private readonly AppDbContext _context;

    public RoomGalleryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RoomGallery?> GetByIdAsync(int id)
    {
        return await _context.RoomGalleries
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<RoomGallery>> GetAllAsync(RoomGalleryFilter filter)
    {
        var query = _context.RoomGalleries.AsQueryable();

        query = query.ApplyFilter(filter)
            .ApplySorting(filter.Sort)
            .ApplyPagination(filter.Paging);

        return await query.ToListAsync();
    }

    public async Task<RoomGallery> AddAsync(RoomGallery roomGallery)
    {
        await _context.RoomGalleries.AddAsync(roomGallery);
        await _context.SaveChangesAsync();
        return roomGallery;
    }

    public async Task<bool> UpdateAsync(RoomGallery roomGallery)
    {
        var exists = await _context.RoomGalleries.AnyAsync(b => b.Id == roomGallery.Id);
        if (exists is false)
            return false;

        _context.RoomGalleries.Update(roomGallery);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.RoomGalleries.FindAsync(id);
        if (entity is null)
            return false;

        _context.RoomGalleries.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
