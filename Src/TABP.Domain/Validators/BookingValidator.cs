using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class BookingValidator : AbstractValidator<Booking>
{
    public BookingValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than zero.");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId must be greater than zero.");

        RuleFor(x => x.RoomId)
            .GreaterThan(0).WithMessage("RoomId must be greater than zero.");

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