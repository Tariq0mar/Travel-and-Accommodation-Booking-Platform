using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class ReviewValidator : AbstractValidator<Review>
{
    public ReviewValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("Id cannot be an empty GUID.");

        RuleFor(x => x.HotelId)
            .NotEmpty()
            .WithMessage("HotelId is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("HotelId cannot be an empty GUID.");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("UserId cannot be an empty GUID.");

        RuleFor(x => x.StarRating)
            .IsInEnum()
            .WithMessage("StarRating must be a value in the Enum");
    }
}