using TABP.Domain.Entities;
using TABP.Domain.QueryFilters;

namespace TABP.Domain.Interfaces.Services;

public interface IHotelGalleryService
{
    Task<HotelGallery> GetByIdAsync(Guid id);
    Task<IEnumerable<HotelGallery>> GetAllAsync(HotelGalleryFilter queryFilter);
    Task<HotelGallery> AddAsync(HotelGallery hotelGallery);
    Task UpdateAsync(HotelGallery hotelGallery);
    Task DeleteAsync(Guid id);
}