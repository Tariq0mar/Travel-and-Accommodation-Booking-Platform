using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class BookingValidator : AbstractValidator<Booking>
{
    public BookingValidator()
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

        RuleFor(x => x.Currency)
            .IsInEnum()
            .WithMessage("Currency must be a value in the Enum");

        RuleFor(x => x.PriceWithoutDiscount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("PriceWithoutDiscount must be greater than or equal 0.");

        RuleFor(x => x.PriceWithDiscount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("PriceWithDiscount must be greater than or equal 0.")
            .LessThanOrEqualTo(x => x.PriceWithoutDiscount)
            .WithMessage("PriceWithDiscount must be less than or equal tp PriceWithoutDiscount.");

        RuleFor(x => x.AdultsCount)
            .GreaterThanOrEqualTo((byte)0)
            .WithMessage("Adults count must be greater than or equal 0.");

        RuleFor(x => x.ChildrenCount)
            .GreaterThanOrEqualTo((byte)0)
            .WithMessage("Adults count must be greater than or equal 0.");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .WithMessage("End date must be after start date");
    }
}