using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class HotelValidator : AbstractValidator<Hotel>
{
    public HotelValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("Id cannot be an empty GUID.");

        RuleFor(x => x.LocationId)
            .NotEmpty()
            .WithMessage("LocationId is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("LocationId cannot be an empty GUID.");

        RuleFor(x => x.StarRating)
            .IsInEnum()
            .WithMessage("StarRating must be a value in the Enum");
    }
}