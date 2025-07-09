using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class AmenityValidator : AbstractValidator<Amenity>
{
    public AmenityValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("Id cannot be an empty GUID.");
    }
}