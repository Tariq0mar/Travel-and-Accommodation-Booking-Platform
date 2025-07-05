using TABP.Domain.Entities;
using TABP.Domain.Filters;

namespace TABP.Domain.Interfaces;

public interface IPaymentRepository
{
    Task<Payment?> GetByIdAsync(Guid id);
    Task<IEnumerable<Payment>> GetAllAsync(BookingFilter filter);
    Task<Payment> AddAsync(Payment payment);
    Task<bool> UpdateAsync(Payment payment);
    Task<bool> DeleteAsync(Payment payment);
}