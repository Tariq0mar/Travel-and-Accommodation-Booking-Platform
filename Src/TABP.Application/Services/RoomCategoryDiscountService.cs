using FluentValidation;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions.ClientExceptions;
using TABP.Domain.Exceptions.ServerExceptions;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Application.Services;

public class RoomCategoryDiscountService : IRoomCategoryDiscountService
{
    private readonly IRoomCategoryDiscountRepository _roomCategoryDiscountRepository;
    private readonly IValidator<RoomCategoryDiscount> _roomCategoryDiscountValidator;

    public RoomCategoryDiscountService(
        IRoomCategoryDiscountRepository roomCategoryDiscountRepository,
        IValidator<RoomCategoryDiscount> roomCategoryDiscountValidator)
    {
        _roomCategoryDiscountRepository = roomCategoryDiscountRepository ?? throw new ArgumentNullException(nameof(roomCategoryDiscountRepository));
        _roomCategoryDiscountValidator = roomCategoryDiscountValidator ?? throw new ArgumentNullException(nameof(roomCategoryDiscountValidator));
    }

    public async Task<RoomCategoryDiscount> GetByIdAsync(int id)
    {
        var roomCategoryDiscount = await _roomCategoryDiscountRepository.GetByIdAsync(id);

        if (roomCategoryDiscount is null)
        {
            throw new NotFoundException($"RoomCategoryDiscount with Id = {id} not found.");
        }

        return roomCategoryDiscount;
    }

    public async Task<IEnumerable<RoomCategoryDiscount>> GetAllAsync(RoomCategoryDiscountFilter queryFilter)
    {
        return await _roomCategoryDiscountRepository.GetAllAsync(queryFilter);
    }

    public async Task<RoomCategoryDiscount> AddAsync(RoomCategoryDiscount roomCategoryDiscount)
    {
        var validation = await _roomCategoryDiscountValidator.ValidateAsync(roomCategoryDiscount);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid room category discount: {errors}");
        }

        var addedRoomCategoryDiscount = await _roomCategoryDiscountRepository.AddAsync(roomCategoryDiscount);

        if (addedRoomCategoryDiscount is null)
        {
            throw new CreationException($"RoomCategoryDiscount could not be created.");
        }

        await _roomCategoryDiscountRepository.SaveChangesAsync();

        return addedRoomCategoryDiscount;
    }

    public async Task UpdateAsync(RoomCategoryDiscount roomCategoryDiscount)
    {
        var validation = await _roomCategoryDiscountValidator.ValidateAsync(roomCategoryDiscount);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid room category discount update: {errors}");
        }

        var success = await _roomCategoryDiscountRepository.UpdateAsync(roomCategoryDiscount);
        if (!success)
        {
            throw new NotFoundException($"RoomCategoryDiscount with Id = {roomCategoryDiscount.Id} not found.");
        }

        await _roomCategoryDiscountRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var success = await _roomCategoryDiscountRepository.DeleteAsync(id);
        if (!success)
        {
            throw new NotFoundException($"RoomCategoryDiscount with Id = {id} not found.");
        }

        await _roomCategoryDiscountRepository.SaveChangesAsync();
    }
}
