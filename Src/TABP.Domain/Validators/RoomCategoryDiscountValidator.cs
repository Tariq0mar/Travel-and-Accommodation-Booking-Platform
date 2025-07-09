using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class RoomCategoryDiscountValidator : AbstractValidator<RoomCategoryDiscount>
{
    public RoomCategoryDiscountValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than zero.");

        RuleFor(x => x.DiscountId)
            .GreaterThan(0).WithMessage("DiscountId must be greater than zero.");

        RuleFor(x => x.RoomCategoryId)
            .GreaterThan(0).WithMessage("RoomCategoryId must be greater than zero.");
    }
}