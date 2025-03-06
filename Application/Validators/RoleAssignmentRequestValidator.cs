using Application.Dtos;
using FluentValidation;

namespace Application.Validators;

public class RoleAssignmentRequestValidator : AbstractValidator<RoleAssignmentRequest>
{
    public RoleAssignmentRequestValidator()
    {
        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");
    }
}