using FluentValidation;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions.ClientExceptions;
using TABP.Domain.Exceptions.ServerExceptions;
using TABP.Domain.Interfaces.Repositories.CRUD;
using TABP.Domain.Interfaces.Services.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Application.Services.CRUD;

public class RoomAmenityService : IRoomAmenityService
{
    private readonly IRoomAmenityRepository _roomAmenityRepository;
    private readonly IValidator<RoomAmenity> _roomAmenityValidator;

    public RoomAmenityService(
        IRoomAmenityRepository roomAmenityRepository,
        IValidator<RoomAmenity> roomAmenityValidator)
    {
        _roomAmenityRepository = roomAmenityRepository ?? throw new ArgumentNullException(nameof(roomAmenityRepository));
        _roomAmenityValidator = roomAmenityValidator ?? throw new ArgumentNullException(nameof(roomAmenityValidator));
    }

    public async Task<RoomAmenity> GetByIdAsync(int id)
    {
        var roomAmenity = await _roomAmenityRepository.GetByIdAsync(id);

        if (roomAmenity is null)
        {
            throw new NotFoundException($"RoomAmenity with Id = {id} not found.");
        }

        return roomAmenity;
    }

    public async Task<IEnumerable<RoomAmenity>> GetAllAsync(RoomAmenityFilter queryFilter)
    {
        return await _roomAmenityRepository.GetAllAsync(queryFilter);
    }

    public async Task<RoomAmenity> AddAsync(RoomAmenity roomAmenity)
    {
        var validation = await _roomAmenityValidator.ValidateAsync(roomAmenity);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid room amenity: {errors}");
        }

        var addedRoomAmenity = await _roomAmenityRepository.AddAsync(roomAmenity);

        if (addedRoomAmenity is null)
        {
            throw new CreationException($"RoomAmenity could not be created.");
        }

        await _roomAmenityRepository.SaveChangesAsync();

        return addedRoomAmenity;
    }

    public async Task UpdateAsync(RoomAmenity roomAmenity)
    {
        var validation = await _roomAmenityValidator.ValidateAsync(roomAmenity);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid room amenity update: {errors}");
        }

        var success = await _roomAmenityRepository.UpdateAsync(roomAmenity);
        if (!success)
        {
            throw new NotFoundException($"RoomAmenity with Id = {roomAmenity.Id} not found.");
        }

        await _roomAmenityRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var success = await _roomAmenityRepository.DeleteAsync(id);
        if (!success)
        {
            throw new NotFoundException($"RoomAmenity with Id = {id} not found.");
        }

        await _roomAmenityRepository.SaveChangesAsync();
    }
}
