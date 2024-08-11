using FluentValidation;

namespace Posts.Application.Features.Posts.Commands.AddComment;

public class AddCommentCommandValidator : AbstractValidator<AddCommentCommand>
{
    public AddCommentCommandValidator()
    {
        RuleFor(comment => comment.Username)
            .NotEmpty().WithMessage("{Username} is required.")
            .NotNull().WithMessage("{Username} can not be null.")
            .MaximumLength(50).WithMessage("{Username} must not exceed 50 characters.");

        RuleFor(comment => comment.Text)
            .NotEmpty().WithMessage("{Text} is required.")
            .NotNull().WithMessage("{Text} can not be null.")
            .MaximumLength(1000).WithMessage("{Text} must not exceed 1000 characters.");
    }
}