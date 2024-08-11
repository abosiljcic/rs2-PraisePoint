using MediatR;

namespace Posts.Application.Features.Posts.Commands.AddLikeCommand;

public class ToggleLikeCommand : IRequest<bool>
{
        public string Username { get; set; }
        public Guid PostId { get; set; }
}