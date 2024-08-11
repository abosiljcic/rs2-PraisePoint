using FluentValidation;

namespace Posts.Application.Features.Posts.Commands.AddLikeCommand;

public class ToggleLikeCommandValidator : AbstractValidator<ToggleLikeCommand>
{
    public ToggleLikeCommandValidator()
    {
            RuleFor(like => like.Username)
                .NotEmpty().WithMessage("{Username} is required.")
                .NotNull().WithMessage("{Username} can not be null.")
                .MaximumLength(50).WithMessage("{Username} must not exceed 50 characters.");

    }
}