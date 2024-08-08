using MediatR;
using Posts.Application.Contracts.Factories;
using Posts.Application.Contracts.Persistence;
using Posts.Application.Features.Posts.Queries.GetPostsByUsername;
using Posts.Application.Features.Posts.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Posts.Application.Features.Posts.Queries.GetPostsByReceiverUsername
{
    public class GetPostsByReceiverUsernameQueryHandler : IRequestHandler<GetPostsByReceiverUsernameQuery, List<PostViewModel>>
    {
        private readonly IPostRepository _repository;
        private readonly IPostViewModelFactory _factory;

        public GetPostsByReceiverUsernameQueryHandler(IPostRepository repository, IPostViewModelFactory factory)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task<List<PostViewModel>> Handle(GetPostsByReceiverUsernameQuery request, CancellationToken cancellationToken)
        {
            var posts = await _repository.GetPostsByReceiverUsername(request.ReceiverUsername);
            return posts.Select(post => _factory.CreateViewModel(post)).ToList();
        }
    }
}
