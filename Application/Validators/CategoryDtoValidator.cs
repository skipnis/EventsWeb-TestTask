using Application.Dtos;
using FluentValidation;

namespace Application.Validators;

public class CategoryDtoValidator: AbstractValidator<CategoryDto>
{
    public CategoryDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(50).WithMessage("Category name should not exceed 50 characters.");
    }
}