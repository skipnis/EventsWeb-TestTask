using Application.Dtos;
using FluentValidation;

namespace Application.Validators;

public class UserRegistrationDtoValidator : AbstractValidator<UserRegistrationDto>
{
    public UserRegistrationDtoValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(255).WithMessage("Username should not exceed 255 characters.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(100).WithMessage("First name should not exceed 100 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(100).WithMessage("Last name should not exceed 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address.")
            .MaximumLength(255).WithMessage("Email should not exceed 255 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .Matches(@"[A-Z]").WithMessage("Password must have at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must have at least one lowercase letter.")
            .Matches(@"[0-9]").WithMessage("Password must have at least one digit.")
            .Matches(@"[\W_]").WithMessage("Password must have at least one non-alphanumeric character.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

        RuleFor(x => x.BirthDate)
            .LessThan(DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("Birth date must be in the past.");
    }
}