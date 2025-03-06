using Application.Dtos;
using FluentValidation;

namespace Application.Validators;

public class PlaceDtoValidator : AbstractValidator<PlaceDto>
{
    public PlaceDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Place name is required.")
            .MaximumLength(50).WithMessage("Place name should not exceed 50 characters.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Place address is required.")
            .MaximumLength(100).WithMessage("Place address should not exceed 100 characters.");
    }
}