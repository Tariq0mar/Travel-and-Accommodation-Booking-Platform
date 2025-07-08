using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class LocationValidator : AbstractValidator<Location>
{
    public LocationValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("Id cannot be an empty GUID.");
    }
}