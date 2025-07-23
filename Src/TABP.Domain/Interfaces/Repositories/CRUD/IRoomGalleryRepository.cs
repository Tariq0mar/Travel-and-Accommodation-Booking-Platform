using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Repositories.CRUD;

public interface IRoomGalleryRepository
{
    Task<RoomGallery?> GetByIdAsync(int id);
    Task<IEnumerable<RoomGallery>> GetAllAsync(RoomGalleryFilter filter);
    Task<RoomGallery> AddAsync(RoomGallery roomGallery);
    Task<bool> UpdateAsync(RoomGallery roomGallery);
    Task<bool> DeleteAsync(int id);
    Task SaveChangesAsync();
}