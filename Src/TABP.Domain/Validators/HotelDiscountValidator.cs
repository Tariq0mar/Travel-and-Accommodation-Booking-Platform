using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class HotelDiscountValidator : AbstractValidator<HotelDiscount>
{
    public HotelDiscountValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than zero.");

        RuleFor(x => x.DiscountId)
            .GreaterThan(0).WithMessage("DiscountId must be greater than zero.");

        RuleFor(x => x.HotelId)
            .GreaterThan(0).WithMessage("HotelId must be greater than zero.")
    }
}