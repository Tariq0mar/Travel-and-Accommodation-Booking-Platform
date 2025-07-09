using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("Id cannot be an empty GUID.");

        RuleFor(x => x.LocationId)
            .NotEmpty()
            .WithMessage("LocationId is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("LocationId cannot be an empty GUID.");

        RuleFor(x => x.UserRole)
            .IsInEnum()
            .WithMessage("UserRole must be a value in the Enum");

        RuleFor(x => x.Gender)
            .IsInEnum()
            .WithMessage("Gender must be a value in the Enum");

        RuleFor(x => x.Age)
            .GreaterThan(0)
            .WithMessage("Age must be greater than 0.")
    }
}