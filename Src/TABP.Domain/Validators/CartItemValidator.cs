using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class CartItemValidator : AbstractValidator<CartItem>
{
    public CartItemValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than zero.");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId must be greater than zero.");

        RuleFor(x => x.RoomId)
            .GreaterThan(0).WithMessage("RoomId must be greater than zero.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0.");
    }
}