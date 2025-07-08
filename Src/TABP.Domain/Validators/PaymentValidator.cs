using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class PaymentValidator : AbstractValidator<Payment>
{
    public PaymentValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("Id cannot be an empty GUID.");

        RuleFor(x => x.BookingId)
            .NotEmpty()
            .WithMessage("BookingId is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("BookingId cannot be an empty GUID.");

        RuleFor(x => x.PaymentMethod)
            .IsInEnum()
            .WithMessage("PaymentMethod must be a value in the Enum");

        RuleFor(x => x.Currency)
            .IsInEnum()
            .WithMessage("Currency must be a value in the Enum");

        RuleFor(x => x.PaymentStatus)
            .IsInEnum()
            .WithMessage("PaymentStatus must be a value in the Enum");
    }
}