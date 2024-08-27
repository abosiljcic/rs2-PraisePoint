using MediatR;
using Microsoft.Extensions.Logging;
using Posts.Application.Contracts.Factories;
using Posts.Application.Contracts.Persistence;

namespace Posts.Application.Features.Posts.Commands.AddComment;

public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, Boolean>
{
    
        private readonly IPostRepository _repository;
        private readonly IPostFactory _factory;
        private readonly ILogger<AddCommentCommandHandler> _logger;

        public AddCommentCommandHandler(IPostRepository postRepository, IPostFactory factory, ILogger<AddCommentCommandHandler> logger)
        {
            _repository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            var post = await _repository.GetPostById(request.PostId);
            if (post == null)
            {
                return false;
            }
            
            post.AddComment(request.Username, request.Text);
            await _repository.UpdateAsync(post);
            
            return true;
        }
}