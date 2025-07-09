using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class HotelValidator : AbstractValidator<Hotel>
{
    public HotelValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than zero.");

        RuleFor(x => x.LocationId)
            .GreaterThan(0).WithMessage("LocationId must be greater than zero.");

        RuleFor(x => x.StarRating)
            .IsInEnum()
            .WithMessage("StarRating must be a value in the Enum");
    }
}