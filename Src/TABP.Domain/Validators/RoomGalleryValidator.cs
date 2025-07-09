using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class RoomGalleryValidator : AbstractValidator<RoomGallery>
{
    public RoomGalleryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than zero.");

        RuleFor(x => x.GalleryId)
            .GreaterThan(0).WithMessage("GalleryId must be greater than zero.");

        RuleFor(x => x.RoomId)
            .GreaterThan(0).WithMessage("RoomId must be greater than zero.");
    }
}