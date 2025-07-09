using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class HotelAmenityValidator : AbstractValidator<HotelAmenity>
{
    public HotelAmenityValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than zero.");

        RuleFor(x => x.AmenityId)
            .GreaterThan(0).WithMessage("AmenityId must be greater than zero.");

        RuleFor(x => x.HotelId)
            .GreaterThan(0).WithMessage("HotelId must be greater than zero.");
    }
}