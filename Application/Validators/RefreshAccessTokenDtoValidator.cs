using Application.Dtos;
using FluentValidation;

namespace Application.Validators;

public class RefreshAccessTokenDtoValidator : AbstractValidator<RefreshAccessTokenDto>
{
    public RefreshAccessTokenDtoValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh token is required.");
    }
}