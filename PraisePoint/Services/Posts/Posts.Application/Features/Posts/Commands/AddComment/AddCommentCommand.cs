using MediatR;

namespace Posts.Application.Features.Posts.Commands.AddComment;

public class AddCommentCommand : IRequest<bool>
{
    
        public string Username { get; set; }
        public Guid PostId { get; set; }
        public string Text { get; set; }
}