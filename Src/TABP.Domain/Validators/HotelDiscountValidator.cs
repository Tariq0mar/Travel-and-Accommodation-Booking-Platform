using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class HotelDiscountValidator : AbstractValidator<HotelDiscount>
{
    public HotelDiscountValidator()
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

        RuleFor(x => x.HotelId)
            .NotEmpty()
            .WithMessage("HotelId is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("HotelId cannot be an empty GUID.");
    }
}