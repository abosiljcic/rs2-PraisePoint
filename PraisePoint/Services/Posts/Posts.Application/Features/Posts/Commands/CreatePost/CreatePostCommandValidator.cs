using FluentValidation;

namespace Posts.Application.Features.Posts.Commands.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator() 
        {
            RuleFor(post => post.SenderUsername) 
                .NotEmpty().WithMessage("{SenderUsername} is required.")
                .NotNull().WithMessage("{SenderUsername} can not be null.")
                .MaximumLength(50).WithMessage("{SenderUsername} must not exceed 50 characters.");

            RuleFor(post => post.ReceiverUsername)
                .NotEmpty().WithMessage("{ReceiverUsername} is required.")
                .NotNull().WithMessage("{ReceiverUsername} can not be null.")
                .MaximumLength(50).WithMessage("{ReceiverUsername} must not exceed 50 characters.");

            RuleFor(post => post.SenderUsername)
                .NotEqual(post => post.ReceiverUsername).WithMessage("Sender and receiver can't be the same person.");

            RuleFor(post => post.Points)
                .NotEmpty().WithMessage("{Points} is required.")
                .Must(points => points > 0).WithMessage("{Points} should be greater than zero");

            RuleFor(post => post.Description)
                .NotEmpty().WithMessage("{Description} is required.")
                .NotNull().WithMessage("{Description} can not be null.")
                .MaximumLength(512).WithMessage("{Description} must not exceed 512 characters.");

            RuleFor(post => post.CompanyId)
                .NotEmpty().WithMessage("{CompanyId} is required.")
                .NotNull().WithMessage("{CompanyId} can not be null.");        }
    }
}
