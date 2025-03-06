using Application.Dtos;
using FluentValidation;

namespace Application.Validators;

public class AddEventImageDtoValidator :AbstractValidator<AddEventImageDto>
{
    public AddEventImageDtoValidator()
    {
        RuleFor(x => x.Image)
            .NotNull().WithMessage("Image is required.")
            .Must(image => image.Length > 0).WithMessage("Image must not be empty.")
            .Must(image => image.Length <= 50 * 1024 * 1024)
            .WithMessage("Image size must be less than 50MB.");
    }
}