using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.Infrastructure.contexts;
using TABP.Infrastructure.QueryExtensions;
using TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

namespace TABP.Infrastructure.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly AppDbContext _context;

    public HotelRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Hotel?> GetByIdAsync(int id)
    {
        return await _context.Hotels
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Hotel?> GetByIdFullDetailsAsync(int id)
    {
        return await _context.Hotels
                .Include(h => h.Location)
                .Include(h => h.HotelGalleries).ThenInclude(hg => hg.Gallery)
                .Include(h => h.HotelAmenities).ThenInclude(ha => ha.Amenity)
                .Include(h => h.HotelDiscounts).ThenInclude(hd => hd.Discount)
                .Include(h => h.Rooms)
                    .ThenInclude(r => r.RoomCategory)
                .Include(h => h.Rooms)
                    .ThenInclude(r => r.RoomAmenities)
                        .ThenInclude(ra => ra.Amenity)
                .Include(h => h.Rooms)
                    .ThenInclude(r => r.RoomCategory)
                        .ThenInclude(rc => rc.RoomCategoryDiscounts)
                            .ThenInclude(rcd => rcd.Discount)
                .Include(h => h.Rooms)
                    .ThenInclude(r => r.RoomGalleries)
                        .ThenInclude(rg => rg.Gallery)
                .Include(h => h.Reviews)
                    .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task<IEnumerable<Hotel>> GetAllAsync(HotelFilter filter)
    {
        var query = _context.Hotels.AsQueryable();

        query = query.ApplyFilter(filter)
            .ApplySorting(filter.Sort)
            .ApplyPagination(filter.Paging);

        return await query.ToListAsync();
    }

    public async Task<Hotel> AddAsync(Hotel hotel)
    {
        await _context.Hotels.AddAsync(hotel);
        await _context.SaveChangesAsync();
        return hotel;
    }

    public async Task<bool> UpdateAsync(Hotel hotel)
    {
        var exists = await _context.Hotels.AnyAsync(b => b.Id == hotel.Id);
        if (exists is false)
            return false;

        _context.Hotels.Update(hotel);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Hotels.FindAsync(id);
        if (entity is null)
            return false;

        _context.Hotels.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}