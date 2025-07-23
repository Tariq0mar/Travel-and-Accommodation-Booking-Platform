using FluentValidation;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions.ClientExceptions;
using TABP.Domain.Exceptions.ServerExceptions;
using TABP.Domain.Interfaces.Repositories.CRUD;
using TABP.Domain.Interfaces.Services.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Application.Services.CRUD;

public class AmenityService : IAmenityService
{
    private readonly IAmenityRepository _amenityRepository;
    private readonly IValidator<Amenity> _amenityValidator;

    public AmenityService(
        IAmenityRepository amenityRepository,
        IValidator<Amenity> amenityValidator)
    {
        _amenityRepository = amenityRepository ?? throw new ArgumentNullException(nameof(amenityRepository));
        _amenityValidator = amenityValidator ?? throw new ArgumentNullException(nameof(amenityValidator));
    }

    public async Task<Amenity> GetByIdAsync(int id)
    {
        var amenity = await _amenityRepository.GetByIdAsync(id);

        if (amenity is null)
        {
            throw new NotFoundException($"Amenity with Id = {id} not found.");
        }

        return amenity;
    }

    public async Task<IEnumerable<Amenity>> GetAllAsync(AmenityFilter queryFilter)
    {
        return await _amenityRepository.GetAllAsync(queryFilter);
    }

    public async Task<Amenity> AddAsync(Amenity amenity)
    {
        var validation = await _amenityValidator.ValidateAsync(amenity);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid amenity: {errors}");
        }

        var addedAmenity = await _amenityRepository.AddAsync(amenity);

        if (addedAmenity is null)
        {
            throw new CreationException($"Amenity could not be created.");
        }

        await _amenityRepository.SaveChangesAsync();

        return addedAmenity;
    }

    public async Task UpdateAsync(Amenity amenity)
    {
        var validation = await _amenityValidator.ValidateAsync(amenity);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid amenity update: {errors}");
        }

        var success = await _amenityRepository.UpdateAsync(amenity);
        if (!success)
        {
            throw new NotFoundException($"Amenity with Id = {amenity.Id} not found.");
        }

        await _amenityRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var success = await _amenityRepository.DeleteAsync(id);
        if (!success)
        {
            throw new NotFoundException($"Amenity with Id = {id} not found.");
        }

        await _amenityRepository.SaveChangesAsync();
    }
}
