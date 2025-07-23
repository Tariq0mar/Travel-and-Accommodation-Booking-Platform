using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.Infrastructure.contexts;
using TABP.Infrastructure.QueryExtensions;
using TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

namespace TABP.Infrastructure.Repositories;

public class AmenityRepository : IAmenityRepository
{
    private readonly AppDbContext _context;

    public AmenityRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Amenity?> GetByIdAsync(int id)
    {
        return await _context.Amenities
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Amenity>> GetAllAsync(AmenityFilter filter)
    {
        var query = _context.Amenities.AsQueryable();

        query = query.ApplyFilter(filter)
            .ApplySorting(filter.Sort)
            .ApplyPagination(filter.Paging);

        return await query.ToListAsync();
    }

    public async Task<Amenity> AddAsync(Amenity amenity)
    {
        await _context.Amenities.AddAsync(amenity);
        await _context.SaveChangesAsync();
        return amenity;
    }

    public async Task<bool> UpdateAsync(Amenity amenity)
    {
        var exists = await _context.Amenities.AnyAsync(b => b.Id == amenity.Id);
        if (exists is false)
            return false;

        _context.Amenities.Update(amenity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Amenities.FindAsync(id);
        if (entity is null)
            return false;

        _context.Amenities.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}