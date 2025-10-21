using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class RoomAmenityValidator : AbstractValidator<RoomAmenity>
{
    public RoomAmenityValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than zero.");

        RuleFor(x => x.AmenityId)
            .GreaterThan(0).WithMessage("AmenityId must be greater than zero.");

        RuleFor(x => x.RoomId)
            .GreaterThan(0).WithMessage("RoomId must be greater than zero.");
    }
}