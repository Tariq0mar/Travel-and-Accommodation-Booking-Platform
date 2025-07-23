using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Services.CRUD;

public interface IPaymentService
{
    Task<Payment> GetByIdAsync(int id);
    Task<IEnumerable<Payment>> GetAllAsync(PaymentFilter queryFilter);
    Task<Payment> AddAsync(Payment payment);
    Task UpdateAsync(Payment payment);
    Task DeleteAsync(int id);
}