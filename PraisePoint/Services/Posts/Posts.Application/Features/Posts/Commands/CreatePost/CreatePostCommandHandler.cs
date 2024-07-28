using MediatR;
using Microsoft.Extensions.Logging;
using Posts.Application.Contracts.Factories;
using Posts.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Posts.Application.Features.Posts.Commands.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
    {
        private readonly IPostRepository _repository;
        private readonly IPostFactory _factory;
        private readonly ILogger<CreatePostCommandHandler> _logger;

        public CreatePostCommandHandler(IPostRepository repository, IPostFactory factory, ILogger<CreatePostCommandHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var postEntity = _factory.Create(request);
            //treba ovom entitetu da dodamo info iz usera
            var newPost = await _repository.AddAsync(postEntity);

            _logger.LogInformation($"Post {newPost.Id} is succesfully created");

            return newPost.Id;
        }
    }
}
