using MediatR;
using Microsoft.Extensions.Logging;
using Posts.Application.Contracts.Persistence;

namespace Posts.Application.Features.Posts.Commands.AddLikeCommand;

public class ToggleLikeCommandHandler : IRequestHandler<ToggleLikeCommand, Boolean>
{
    private readonly IPostRepository _repository;
    private readonly ILogger<ToggleLikeCommandHandler> _logger;

    public ToggleLikeCommandHandler(IPostRepository postRepository, ILogger<ToggleLikeCommandHandler> logger)
    {
        _repository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> Handle(ToggleLikeCommand request, CancellationToken cancellationToken)
    {
        var post = await _repository.GetPostById(request.PostId);
        if (post == null)
        {
            _logger.LogWarning("Post with given ID not found.");
            return false;
        }

        post.ToggleLiked(request.Username);
        await _repository.UpdateAsync(post);
        _logger.LogInformation($"Toggled like for a post with id: {request.PostId}, by user: {request.Username}");
            
        return true;
    }
}    