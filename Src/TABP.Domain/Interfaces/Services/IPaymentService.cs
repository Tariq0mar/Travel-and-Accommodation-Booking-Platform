using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Services;

public interface IPaymentService
{
    Task<Payment> GetByIdAsync(Guid id);
    Task<IEnumerable<Payment>> GetAllAsync(PaymentFilter queryFilter);
    Task<Payment> AddAsync(Payment payment);
    Task UpdateAsync(Payment payment);
    Task DeleteAsync(Guid id);
}