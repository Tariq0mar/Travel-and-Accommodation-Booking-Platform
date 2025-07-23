using FluentValidation;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions.ClientExceptions;
using TABP.Domain.Exceptions.ServerExceptions;
using TABP.Domain.Interfaces.Repositories.CRUD;
using TABP.Domain.Interfaces.Services.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Application.Services.CRUD;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;
    private readonly IValidator<Location> _locationValidator;

    public LocationService(
        ILocationRepository locationRepository,
        IValidator<Location> locationValidator)
    {
        _locationRepository = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
        _locationValidator = locationValidator ?? throw new ArgumentNullException(nameof(locationValidator));
    }

    public async Task<Location> GetByIdAsync(int id)
    {
        var location = await _locationRepository.GetByIdAsync(id);

        if (location is null)
        {
            throw new NotFoundException($"Location with Id = {id} not found.");
        }

        return location;
    }

    public async Task<IEnumerable<Location>> GetAllAsync(LocationFilter queryFilter)
    {
        return await _locationRepository.GetAllAsync(queryFilter);
    }

    public async Task<Location> AddAsync(Location location)
    {
        var validation = await _locationValidator.ValidateAsync(location);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid location: {errors}");
        }

        var addedLocation = await _locationRepository.AddAsync(location);

        if (addedLocation is null)
        {
            throw new CreationException($"Location could not be created.");
        }

        await _locationRepository.SaveChangesAsync();

        return addedLocation;
    }

    public async Task UpdateAsync(Location location)
    {
        var validation = await _locationValidator.ValidateAsync(location);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid location update: {errors}");
        }

        var success = await _locationRepository.UpdateAsync(location);
        if (!success)
        {
            throw new NotFoundException($"Location with Id = {location.Id} not found.");
        }

        await _locationRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var success = await _locationRepository.DeleteAsync(id);
        if (!success)
        {
            throw new NotFoundException($"Location with Id = {id} not found.");
        }

        await _locationRepository.SaveChangesAsync();
    }
}
