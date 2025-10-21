using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class HotelGalleryValidator : AbstractValidator<HotelGallery>
{
    public HotelGalleryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than zero.");

        RuleFor(x => x.GalleryId)
            .GreaterThan(0).WithMessage("GalleryId must be greater than zero.");

        RuleFor(x => x.HotelId)
            .GreaterThan(0).WithMessage("HotelId must be greater than zero.");
    }
}