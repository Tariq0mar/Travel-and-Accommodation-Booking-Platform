using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.Infrastructure.contexts;
using TABP.Infrastructure.QueryExtensions;
using TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

namespace TABP.Infrastructure.Repositories.CRUD;

public class HotelDiscountRepository : IHotelDiscountRepository
{
    private readonly AppDbContext _context;

    public HotelDiscountRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<HotelDiscount?> GetByIdAsync(int id)
    {
        return await _context.HotelDiscounts
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<HotelDiscount>> GetAllAsync(HotelDiscountFilter filter)
    {
        var query = _context.HotelDiscounts.AsQueryable();

        query = query.ApplyFilter(filter)
            .ApplySorting(filter.Sort)
            .ApplyPagination(filter.Paging);

        return await query.ToListAsync();
    }

    public async Task<HotelDiscount> AddAsync(HotelDiscount hotelDiscount)
    {
        await _context.HotelDiscounts.AddAsync(hotelDiscount);
        await _context.SaveChangesAsync();
        return hotelDiscount;
    }

    public async Task<bool> UpdateAsync(HotelDiscount hotelDiscount)
    {
        var exists = await _context.HotelDiscounts.AnyAsync(b => b.Id == hotelDiscount.Id);
        if (exists is false)
            return false;

        _context.HotelDiscounts.Update(hotelDiscount);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.HotelDiscounts.FindAsync(id);
        if (entity is null)
            return false;

        _context.HotelDiscounts.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
