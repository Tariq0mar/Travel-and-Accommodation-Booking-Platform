using FluentValidation;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions.ClientExceptions;
using TABP.Domain.Exceptions.ServerExceptions;
using TABP.Domain.Interfaces.Repositories.CRUD;
using TABP.Domain.Interfaces.Services.CRUD;
using TABP.Domain.QueryFilters.EntitiesFilters;

namespace TABP.Application.Services.CRUD;

public class GalleryService : IGalleryService
{
    private readonly IGalleryRepository _galleryRepository;
    private readonly IValidator<Gallery> _galleryValidator;

    public GalleryService(
        IGalleryRepository galleryRepository,
        IValidator<Gallery> galleryValidator)
    {
        _galleryRepository = galleryRepository ?? throw new ArgumentNullException(nameof(galleryRepository));
        _galleryValidator = galleryValidator ?? throw new ArgumentNullException(nameof(galleryValidator));
    }

    public async Task<Gallery> GetByIdAsync(int id)
    {
        var gallery = await _galleryRepository.GetByIdAsync(id);

        if (gallery is null)
        {
            throw new NotFoundException($"Gallery with Id = {id} not found.");
        }

        return gallery;
    }

    public async Task<IEnumerable<Gallery>> GetAllAsync(GalleryFilter queryFilter)
    {
        return await _galleryRepository.GetAllAsync(queryFilter);
    }

    public async Task<Gallery> AddAsync(Gallery gallery)
    {
        var validation = await _galleryValidator.ValidateAsync(gallery);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid gallery: {errors}");
        }

        var addedGallery = await _galleryRepository.AddAsync(gallery);

        if (addedGallery is null)
        {
            throw new CreationException($"Gallery could not be created.");
        }

        await _galleryRepository.SaveChangesAsync();

        return addedGallery;
    }

    public async Task UpdateAsync(Gallery gallery)
    {
        var validation = await _galleryValidator.ValidateAsync(gallery);
        if (!validation.IsValid)
        {
            var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Invalid gallery update: {errors}");
        }

        var success = await _galleryRepository.UpdateAsync(gallery);
        if (!success)
        {
            throw new NotFoundException($"Gallery with Id = {gallery.Id} not found.");
        }

        await _galleryRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var success = await _galleryRepository.DeleteAsync(id);
        if (!success)
        {
            throw new NotFoundException($"Gallery with Id = {id} not found.");
        }

        await _galleryRepository.SaveChangesAsync();
    }
}
