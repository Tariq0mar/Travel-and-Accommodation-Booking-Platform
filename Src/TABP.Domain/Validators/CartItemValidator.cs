using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class CartItemValidator : AbstractValidator<CartItem>
{
    public CartItemValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("Id cannot be an empty GUID.");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("UserId cannot be an empty GUID.");

        RuleFor(x => x.RoomId)
            .NotEmpty()
            .WithMessage("RoomId is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("RoomId cannot be an empty GUID.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0.");
    }
}