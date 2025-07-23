using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.Infrastructure.contexts;
using TABP.Infrastructure.QueryExtensions;
using TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

namespace TABP.Infrastructure.Repositories.CRUD;

public class HotelGalleryRepository : IHotelGalleryRepository
{
    private readonly AppDbContext _context;

    public HotelGalleryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<HotelGallery?> GetByIdAsync(int id)
    {
        return await _context.HotelGalleries
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<HotelGallery>> GetAllAsync(HotelGalleryFilter filter)
    {
        var query = _context.HotelGalleries.AsQueryable();

        query = query.ApplyFilter(filter)
            .ApplySorting(filter.Sort)
            .ApplyPagination(filter.Paging);

        return await query.ToListAsync();
    }

    public async Task<HotelGallery> AddAsync(HotelGallery hotelGallery)
    {
        await _context.HotelGalleries.AddAsync(hotelGallery);
        await _context.SaveChangesAsync();
        return hotelGallery;
    }

    public async Task<bool> UpdateAsync(HotelGallery hotelGallery)
    {
        var exists = await _context.HotelGalleries.AnyAsync(b => b.Id == hotelGallery.Id);
        if (exists is false)
            return false;

        _context.HotelGalleries.Update(hotelGallery);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.HotelGalleries.FindAsync(id);
        if (entity is null)
            return false;

        _context.HotelGalleries.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
