using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class RoomGalleryValidator : AbstractValidator<RoomGallery>
{
    public RoomGalleryValidator()
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

        RuleFor(x => x.RoomId)
            .NotEmpty()
            .WithMessage("RoomId is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("RoomId cannot be an empty GUID.");
    }
}