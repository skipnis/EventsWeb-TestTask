using Application.Dtos;
using FluentValidation;

namespace Application.Validators;

public class EventCreationDtoValidator: AbstractValidator<EventCreationDto>
{
    public EventCreationDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Event title is required.")
            .MaximumLength(100).WithMessage("Event title should not exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Event description should not exceed 1000 characters.")
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.Place)
            .NotNull().WithMessage("Place is required.");

        RuleFor(x => x.Category)
            .NotNull().WithMessage("Category is required.");

        RuleFor(x => x.Date)
            .GreaterThan(DateTime.Now).WithMessage("Event date must be in the future.");

        RuleFor(x => x.MaximumParticipants)
            .GreaterThan(0).WithMessage("Maximum participants must be greater than 0.");
    }
}