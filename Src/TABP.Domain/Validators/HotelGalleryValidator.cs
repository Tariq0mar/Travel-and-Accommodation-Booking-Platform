using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class HotelGalleryValidator : AbstractValidator<HotelGallery>
{
    public HotelGalleryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("Id cannot be an empty GUID.");

        RuleFor(x => x.GalleryId)
            .NotEmpty()
            .WithMessage("GalleryId is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("GalleryId cannot be an empty GUID.");

        RuleFor(x => x.HotelId)
            .NotEmpty()
            .WithMessage("HotelId is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("HotelId cannot be an empty GUID.");
    }
}