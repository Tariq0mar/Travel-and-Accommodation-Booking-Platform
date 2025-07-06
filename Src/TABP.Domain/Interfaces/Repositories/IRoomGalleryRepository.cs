using TABP.Domain.Entities;
using TABP.Domain.QueryFilters;

namespace TABP.Domain.Interfaces.Repositories;

public interface IRoomGalleryRepository
{
    Task<RoomGallery?> GetByIdAsync(Guid id);
    Task<IEnumerable<RoomGallery>> GetAllAsync(RoomGalleryFilter filter);
    Task<RoomGallery> AddAsync(RoomGallery roomGallery);
    Task<bool> UpdateAsync(RoomGallery roomGallery);
    Task<bool> DeleteAsync(Guid id);
}