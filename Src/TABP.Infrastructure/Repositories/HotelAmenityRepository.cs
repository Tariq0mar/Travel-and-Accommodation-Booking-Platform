using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.Infrastructure.contexts;
using TABP.Infrastructure.QueryExtensions;
using TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

namespace TABP.Infrastructure.Repositories;

public class HotelAmenityRepository : IHotelAmenityRepository
{
    private readonly AppDbContext _context;

    public HotelAmenityRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<HotelAmenity?> GetByIdAsync(int id)
    {
        return await _context.HotelAmenities
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<HotelAmenity>> GetAllAsync(HotelAmenityFilter filter)
    {
        var query = _context.HotelAmenities.AsQueryable();

        query = query.ApplyFilter(filter)
            .ApplySorting(filter.Sort)
            .ApplyPagination(filter.Paging);

        return await query.ToListAsync();
    }

    public async Task<HotelAmenity> AddAsync(HotelAmenity hotelAmenity)
    {
        await _context.HotelAmenities.AddAsync(hotelAmenity);
        await _context.SaveChangesAsync();
        return hotelAmenity;
    }

    public async Task<bool> UpdateAsync(HotelAmenity hotelAmenity)
    {
        var exists = await _context.HotelAmenities.AnyAsync(b => b.Id == hotelAmenity.Id);
        if (exists is false)
            return false;

        _context.HotelAmenities.Update(hotelAmenity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.HotelAmenities.FindAsync(id);
        if (entity is null)
            return false;

        _context.HotelAmenities.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
