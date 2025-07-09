using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class UserDiscountValidator : AbstractValidator<UserDiscount>
{
    public UserDiscountValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than zero.");

        RuleFor(x => x.DiscountId)
            .GreaterThan(0).WithMessage("DiscountId must be greater than zero.");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId must be greater than zero.");
    }
}