using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class GalleryValidator : AbstractValidator<Gallery>
{
    public GalleryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than zero.");
    }
}