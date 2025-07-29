using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.QueryFilters.EntitiesFilters;
using TABP.Infrastructure.contexts;
using TABP.Infrastructure.QueryExtensions;
using TABP.Infrastructure.QueryExtensions.EntityFilterExtensions;

namespace TABP.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _context;
    private IDbContextTransaction _currentTransaction;

    public BookingRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Booking?> GetByIdAsync(int id)
    {
        return await _context.Bookings
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Booking>> GetAllAsync(BookingFilter filter)
    {
        var query = _context.Bookings.AsQueryable();

        query = query.ApplyFilter(filter)
            .ApplySorting(filter.Sort)
            .ApplyPagination(filter.Paging);

        return await query.ToListAsync();
    }

    public async Task<Booking> AddAsync(Booking booking)
    {
        await _context.Bookings.AddAsync(booking);
        await _context.SaveChangesAsync();
        return booking;
    }

    public async Task<bool> UpdateAsync(Booking booking)
    {
        var exists = await _context.Bookings.AnyAsync(b => b.Id == booking.Id);
        if (exists is false)
            return false;

        _context.Bookings.Update(booking);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Bookings.FindAsync(id);
        if (entity is null)
            return false;

        _context.Bookings.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime startDate, DateTime endDate)
    {
        return !await _context.Bookings.AnyAsync(b =>
            b.RoomId == roomId &&
            ((startDate >= b.StartDate && startDate < b.EndDate) ||
             (endDate > b.StartDate && endDate <= b.EndDate) ||
             (startDate <= b.StartDate && endDate >= b.EndDate)));
    }

    public async Task BeginTransactionAsync()
    {
        if (_currentTransaction != null)
            return;

        // Use SERIALIZABLE to prevent concurrent overlapping bookings
        _currentTransaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            if (_currentTransaction == null)
                return;

            await _context.SaveChangesAsync();
            await _currentTransaction.CommitAsync();
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            await _currentTransaction.RollbackAsync();
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }
}