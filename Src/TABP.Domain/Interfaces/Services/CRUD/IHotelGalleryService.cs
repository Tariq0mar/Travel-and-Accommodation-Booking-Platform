using TABP.Domain.Entities;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Domain.Interfaces.Services.CRUD;

public interface IHotelGalleryService
{
    Task<HotelGallery> GetByIdAsync(int id);
    Task<IEnumerable<HotelGallery>> GetAllAsync(HotelGalleryFilter queryFilter);
    Task<HotelGallery> AddAsync(HotelGallery hotelGallery);
    Task UpdateAsync(HotelGallery hotelGallery);
    Task DeleteAsync(int id);
}