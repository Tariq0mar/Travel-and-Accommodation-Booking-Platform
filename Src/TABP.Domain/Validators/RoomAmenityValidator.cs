using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class RoomAmenityValidator : AbstractValidator<RoomAmenity>
{
    public RoomAmenityValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("Id cannot be an empty GUID.");

        RuleFor(x => x.AmenityId)
            .NotEmpty()
            .WithMessage("AmenityId is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("AmenityId cannot be an empty GUID.");

        RuleFor(x => x.RoomId)
            .NotEmpty()
            .WithMessage("RoomId is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("RoomId cannot be an empty GUID.");
    }
}