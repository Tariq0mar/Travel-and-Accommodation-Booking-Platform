using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Services;

public interface IRoomGalleryService
{
    Task<RoomGallery> GetByIdAsync(int id);
    Task<IEnumerable<RoomGallery>> GetAllAsync(RoomGalleryFilter queryFilter);
    Task<RoomGallery> AddAsync(RoomGallery roomGallery);
    Task UpdateAsync(RoomGallery roomGallery);
    Task DeleteAsync(int id);
}