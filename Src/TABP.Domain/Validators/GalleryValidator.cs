using FluentValidation;
using TABP.Domain.Entities;

namespace TABP.Domain.Validators;

public class GalleryValidator : AbstractValidator<Gallery>
{
    public GalleryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("Id cannot be an empty GUID.");
    }
}