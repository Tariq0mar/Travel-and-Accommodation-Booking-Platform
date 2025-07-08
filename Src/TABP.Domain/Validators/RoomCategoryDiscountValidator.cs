using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class RoomCategoryDiscountValidator : AbstractValidator<RoomCategoryDiscount>
{
    public RoomCategoryDiscountValidator()
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

        RuleFor(x => x.RoomCategoryId)
            .NotEmpty()
            .WithMessage("RoomCategoryId is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("RoomCategoryId cannot be an empty GUID.");
    }
}