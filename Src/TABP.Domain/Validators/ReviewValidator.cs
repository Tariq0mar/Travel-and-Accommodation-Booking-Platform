using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class ReviewValidator : AbstractValidator<Review>
{
    public ReviewValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than zero.");

        RuleFor(x => x.HotelId)
            .GreaterThan(0).WithMessage("HotelId must be greater than zero.");

        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("UserId must be greater than zero.");

        RuleFor(x => x.StarRating)
            .IsInEnum()
            .WithMessage("StarRating must be a value in the Enum");
    }
}