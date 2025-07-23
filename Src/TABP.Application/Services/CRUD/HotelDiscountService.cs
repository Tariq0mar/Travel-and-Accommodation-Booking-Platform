using FluentValidation;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions.ClientExceptions;
using TABP.Domain.Exceptions.ServerExceptions;
using TABP.Domain.Interfaces.Repositories.CRUD;
using TABP.Domain.Interfaces.Services.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Application.Services.CRUD;

public class HotelDiscountService : IHotelDiscountService
{
    private readonly IHotelDiscountRepository _hotelDiscountRepository;
    private readonly IValidator<HotelDiscount> _hotelDiscountValidator;

    public HotelDiscountService(
        IHotelDiscountRepository hotelDiscountRepository,
        IValidator<HotelDiscount> hotelDiscountValidator)
    {
        _hotelDiscountRepository = hotelDiscountRepository ?? throw new ArgumentNullException(nameof(hotelDiscountRepository));
        _hotelDiscountValidator = hotelDiscountValidator ?? throw new ArgumentNullException(nameof(hotelDiscountValidator));
    }

    public async Task<HotelDiscount> GetByIdAsync(int id)
    {
        var hotelDiscount = await _hotelDiscountRepository.GetByIdAsync(id);

        if (hotelDiscount is null)
        {
            throw new NotFoundException($"HotelDiscount with Id = {id} not found.");
        }

        return hotelDiscount;
    }

    public async Task<IEnumerable<HotelDiscount>> GetAllAsync(HotelDiscountFilter queryFilter)
    {
        return await _hotelDiscountRepository.GetAllAsync(queryFilter);
    }

    public async Task<HotelDiscount> AddAsync(HotelDiscount hotelDiscount)
    {
        var validation = await _hotelDiscountValidator.ValidateAsync(hotelDiscount);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid hotel discount: {errors}");
        }

        var addedHotelDiscount = await _hotelDiscountRepository.AddAsync(hotelDiscount);

        if (addedHotelDiscount is null)
        {
            throw new CreationException($"HotelDiscount could not be created.");
        }

        await _hotelDiscountRepository.SaveChangesAsync();

        return addedHotelDiscount;
    }

    public async Task UpdateAsync(HotelDiscount hotelDiscount)
    {
        var validation = await _hotelDiscountValidator.ValidateAsync(hotelDiscount);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid hotel discount update: {errors}");
        }

        var success = await _hotelDiscountRepository.UpdateAsync(hotelDiscount);
        if (!success)
        {
            throw new NotFoundException($"HotelDiscount with Id = {hotelDiscount.Id} not found.");
        }

        await _hotelDiscountRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var success = await _hotelDiscountRepository.DeleteAsync(id);
        if (!success)
        {
            throw new NotFoundException($"HotelDiscount with Id = {id} not found.");
        }

        await _hotelDiscountRepository.SaveChangesAsync();
    }
}
