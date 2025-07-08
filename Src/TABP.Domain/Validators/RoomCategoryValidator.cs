using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class RoomCategoryValidator : AbstractValidator<RoomCategory>
{
    public RoomCategoryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("Id cannot be an empty GUID.");

        RuleFor(x => x.Category)
            .IsInEnum()
            .WithMessage("RoomCategoryType must be a value in the Enum");

        RuleFor(x => x.Currency)
            .IsInEnum()
            .WithMessage("Currency must be a value in the Enum");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price must be greater than or equal 0.");

        RuleFor(x => x.AdultsCount)
            .GreaterThanOrEqualTo((byte)0)
            .WithMessage("Adults count must be greater than or equal 0.");

        RuleFor(x => x.ChildrenCount)
            .GreaterThanOrEqualTo((byte)0)
            .WithMessage("Adults count must be greater than or equal 0.");
    }
}