using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class RoomValidator : AbstractValidator<Room>
{
    public RoomValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("Id cannot be an empty GUID.");

        RuleFor(x => x.HotelId)
            .NotEmpty()
            .WithMessage("HotelId is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("HotelId cannot be an empty GUID.");

        RuleFor(x => x.RoomCategoryId)
            .NotEmpty()
            .WithMessage("RoomCategoryId is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("RoomCategoryId cannot be an empty GUID.");
    }
}