using FluentValidation;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions.ClientExceptions;
using TABP.Domain.Exceptions.ServerExceptions;
using TABP.Domain.Interfaces.Repositories.CRUD;
using TABP.Domain.Interfaces.Services.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Application.Services.CRUD;

public class RoomCategoryService : IRoomCategoryService
{
    private readonly IRoomCategoryRepository _roomCategoryRepository;
    private readonly IValidator<RoomCategory> _roomCategoryValidator;

    public RoomCategoryService(
        IRoomCategoryRepository roomCategoryRepository,
        IValidator<RoomCategory> roomCategoryValidator)
    {
        _roomCategoryRepository = roomCategoryRepository ?? throw new ArgumentNullException(nameof(roomCategoryRepository));
        _roomCategoryValidator = roomCategoryValidator ?? throw new ArgumentNullException(nameof(roomCategoryValidator));
    }

    public async Task<RoomCategory> GetByIdAsync(int id)
    {
        var roomCategory = await _roomCategoryRepository.GetByIdAsync(id);

        if (roomCategory is null)
        {
            throw new NotFoundException($"RoomCategory with Id = {id} not found.");
        }

        return roomCategory;
    }

    public async Task<IEnumerable<RoomCategory>> GetAllAsync(RoomCategoryFilter queryFilter)
    {
        return await _roomCategoryRepository.GetAllAsync(queryFilter);
    }

    public async Task<RoomCategory> AddAsync(RoomCategory roomCategory)
    {
        var validation = await _roomCategoryValidator.ValidateAsync(roomCategory);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid room category: {errors}");
        }

        var addedRoomCategory = await _roomCategoryRepository.AddAsync(roomCategory);

        if (addedRoomCategory is null)
        {
            throw new CreationException($"RoomCategory could not be created.");
        }

        await _roomCategoryRepository.SaveChangesAsync();

        return addedRoomCategory;
    }

    public async Task UpdateAsync(RoomCategory roomCategory)
    {
        var validation = await _roomCategoryValidator.ValidateAsync(roomCategory);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid room category update: {errors}");
        }

        var success = await _roomCategoryRepository.UpdateAsync(roomCategory);
        if (!success)
        {
            throw new NotFoundException($"RoomCategory with Id = {roomCategory.Id} not found.");
        }

        await _roomCategoryRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var success = await _roomCategoryRepository.DeleteAsync(id);
        if (!success)
        {
            throw new NotFoundException($"RoomCategory with Id = {id} not found.");
        }

        await _roomCategoryRepository.SaveChangesAsync();
    }
}
