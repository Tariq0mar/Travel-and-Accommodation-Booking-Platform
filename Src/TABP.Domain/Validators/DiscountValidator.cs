using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class DiscountValidator : AbstractValidator<Discount>
{
    public DiscountValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than zero.");

        RuleFor(x => x.DiscountType)
            .IsInEnum()
            .WithMessage("Discount must be a value in the Enum");

        RuleFor(x => x.Currency)
            .IsInEnum()
            .WithMessage("Currency must be a value in the Enum");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .WithMessage("End date must be after start date");

        RuleFor(x => x.Value)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Value must be greater than or equal 0.");
    }
}