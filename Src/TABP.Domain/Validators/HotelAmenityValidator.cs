using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class HotelAmenityValidator : AbstractValidator<HotelAmenity>
{
    public HotelAmenityValidator()
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

        RuleFor(x => x.HotelId)
            .NotEmpty()
            .WithMessage("HotelId is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("HotelId cannot be an empty GUID.");
    }
}