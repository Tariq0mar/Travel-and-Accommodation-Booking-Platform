using TABP.Domain.Entities;
using TABP.Domain.Filters;

namespace TABP.Domain.Interfaces;

public interface IRoomGalleryRepository
{
    Task<RoomGallery?> GetByIdAsync(Guid id);
    Task<IEnumerable<RoomGallery>> GetAllAsync(RoomGalleryFilter filter);
    Task<RoomGallery> AddAsync(RoomGallery roomGallery);
    Task<bool> UpdateAsync(RoomGallery roomGallery);
    Task<bool> DeleteAsync(RoomGallery roomGallery);
}