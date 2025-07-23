using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.Infrastructure.contexts;
using TABP.Infrastructure.QueryExtensions;
using TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

namespace TABP.Infrastructure.Repositories.CRUD;

public class GalleryRepository : IGalleryRepository
{
    private readonly AppDbContext _context;

    public GalleryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Gallery?> GetByIdAsync(int id)
    {
        return await _context.Galleries
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Gallery>> GetAllAsync(GalleryFilter filter)
    {
        var query = _context.Galleries.AsQueryable();

        query = query.ApplyFilter(filter)
            .ApplySorting(filter.Sort)
            .ApplyPagination(filter.Paging);

        return await query.ToListAsync();
    }

    public async Task<Gallery> AddAsync(Gallery gallery)
    {
        await _context.Galleries.AddAsync(gallery);
        await _context.SaveChangesAsync();
        return gallery;
    }

    public async Task<bool> UpdateAsync(Gallery gallery)
    {
        var exists = await _context.Galleries.AnyAsync(b => b.Id == gallery.Id);
        if (exists is false)
            return false;

        _context.Galleries.Update(gallery);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Galleries.FindAsync(id);
        if (entity is null)
            return false;

        _context.Galleries.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}