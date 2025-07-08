using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class UserDiscountValidator : AbstractValidator<UserDiscount>
{
    public UserDiscountValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("Id cannot be an empty GUID.");

        RuleFor(x => x.DiscountId)
            .NotEmpty()
            .WithMessage("DiscountId is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("DiscountId cannot be an empty GUID.");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("UserId cannot be an empty GUID.");
    }
}