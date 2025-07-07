using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Repositories;

public interface IHotelGalleryRepository
{
    Task<HotelGallery?> GetByIdAsync(Guid id);
    Task<IEnumerable<HotelGallery>> GetAllAsync(HotelGalleryFilter filter);
    Task<HotelGallery> AddAsync(HotelGallery hotelGallery);
    Task<bool> UpdateAsync(HotelGallery hotelGallery);
    Task<bool> DeleteAsync(Guid id);
}