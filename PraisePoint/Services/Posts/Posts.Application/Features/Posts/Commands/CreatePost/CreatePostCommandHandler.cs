using MediatR;
using Microsoft.Extensions.Logging;
using Posts.Application.Contracts.Factories;
using Posts.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Posts.Application.Contracts.Infrastructure;

namespace Posts.Application.Features.Posts.Commands.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
    {
        private readonly IPostRepository _repository;
        private readonly IPostFactory _factory;
        private readonly IRewardService _rewardService;
        private readonly ILogger<CreatePostCommandHandler> _logger;

        public CreatePostCommandHandler(IPostRepository repository, IPostFactory factory, ILogger<CreatePostCommandHandler> logger, IRewardService rewardService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _rewardService = rewardService ?? throw new ArgumentNullException(nameof(rewardService));
        }

        public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreatePostCommandHandler.Handle({request}");
            var pointsInfo = await _rewardService.GetPointsForUser(request.SenderUsername);
            
            if (pointsInfo == null) return Guid.Empty;
            if (pointsInfo.Budget < request.Points) return Guid.Empty;
            
            var postEntity = _factory.Create(request);
            postEntity.CreatedDate = DateTime.UtcNow;
            var newPost = await _repository.AddAsync(postEntity);

            _logger.LogInformation($"Post {newPost.Id} is succesfully created");
            return newPost.Id;
        }
    }
}
