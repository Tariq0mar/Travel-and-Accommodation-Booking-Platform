using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than zero.");

        RuleFor(x => x.LocationId)
            .GreaterThan(0).WithMessage("LocationId must be greater than zero.");

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