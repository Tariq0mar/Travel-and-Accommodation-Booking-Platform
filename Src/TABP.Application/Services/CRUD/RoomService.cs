using FluentValidation;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions.ClientExceptions;
using TABP.Domain.Exceptions.ServerExceptions;
using TABP.Domain.Interfaces.Repositories.CRUD;
using TABP.Domain.Interfaces.Services.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Application.Services.CRUD;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;
    private readonly IValidator<Room> _roomValidator;

    public RoomService(
        IRoomRepository roomRepository,
        IValidator<Room> roomValidator)
    {
        _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
        _roomValidator = roomValidator ?? throw new ArgumentNullException(nameof(roomValidator));
    }

    public async Task<Room> GetByIdAsync(int id)
    {
        var room = await _roomRepository.GetByIdAsync(id);

        if (room is null)
        {
            throw new NotFoundException($"Room with Id = {id} not found.");
        }

        return room;
    }

    public async Task<IEnumerable<Room>> GetAllAsync(RoomFilter queryFilter)
    {
        return await _roomRepository.GetAllAsync(queryFilter);
    }

    public async Task<Room> AddAsync(Room room)
    {
        var validation = await _roomValidator.ValidateAsync(room);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid room: {errors}");
        }

        var addedRoom = await _roomRepository.AddAsync(room);

        if (addedRoom is null)
        {
            throw new CreationException($"Room could not be created.");
        }

        await _roomRepository.SaveChangesAsync();

        return addedRoom;
    }

    public async Task UpdateAsync(Room room)
    {
        var validation = await _roomValidator.ValidateAsync(room);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid room update: {errors}");
        }

        var success = await _roomRepository.UpdateAsync(room);
        if (!success)
        {
            throw new NotFoundException($"Room with Id = {room.Id} not found.");
        }

        await _roomRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var success = await _roomRepository.DeleteAsync(id);
        if (!success)
        {
            throw new NotFoundException($"Room with Id = {id} not found.");
        }

        await _roomRepository.SaveChangesAsync();
    }
}
