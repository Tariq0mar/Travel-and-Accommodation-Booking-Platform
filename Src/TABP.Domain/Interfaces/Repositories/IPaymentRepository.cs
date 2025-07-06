using TABP.Domain.Entities;
using TABP.Domain.QueryFilters;

namespace TABP.Domain.Interfaces.Repositories;

public interface IPaymentRepository
{
    Task<Payment?> GetByIdAsync(Guid id);
    Task<IEnumerable<Payment>> GetAllAsync(BookingFilter filter);
    Task<Payment> AddAsync(Payment payment);
    Task<bool> UpdateAsync(Payment payment);
    Task<bool> DeleteAsync(Guid id);
}