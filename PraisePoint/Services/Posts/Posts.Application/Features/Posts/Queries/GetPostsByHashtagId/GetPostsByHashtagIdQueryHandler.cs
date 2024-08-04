using MediatR;
using Posts.Application.Contracts.Factories;
using Posts.Application.Contracts.Persistence;
using Posts.Application.Features.Posts.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Posts.Application.Features.Posts.Queries.GetPostsByHashtagId
{
    public class GetPostsByHashtagIdQueryHandler : IRequestHandler<GetPostsByHashtagIdQuery, List<PostViewModel>>
    {
        private readonly IPostRepository _repository;
        private readonly IPostViewModelFactory _factory;

        public GetPostsByHashtagIdQueryHandler(IPostRepository repository, IPostViewModelFactory factory)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task<List<PostViewModel>> Handle(GetPostsByHashtagIdQuery request, CancellationToken cancellationToken)
        {
            var posts = await _repository.GetPostsByHashtagId(request.HashtagId);
            return posts.Select(p => _factory.CreateViewModel(p)).ToList();
        }
    }
}
