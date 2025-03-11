using Application.Dtos;
using FluentValidation;

namespace Application.Validators;

public class RefreshAccessTokenDtoValidator : AbstractValidator<RefreshAccessTokeRequestDto>
{
    public RefreshAccessTokenDtoValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");
        
        RuleFor(x=>x.UserName)
            .NotEmpty().WithMessage("Username is required.");
        
        RuleFor(x=>x.Roles)
            .NotEmpty().WithMessage("Roles are required.");

        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh token is required.");
    }
}