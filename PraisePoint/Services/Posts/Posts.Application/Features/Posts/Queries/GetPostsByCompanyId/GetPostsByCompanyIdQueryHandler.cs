using MediatR;
using Posts.Application.Contracts.Factories;
using Posts.Application.Contracts.Persistence;
using Posts.Application.Features.Posts.Queries.GetPostById;
using Posts.Application.Features.Posts.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Posts.Application.Features.Posts.Queries.GetPostsByCompanyId
{
    public class GetPostsByCompanyIdQueryHandler : IRequestHandler<GetPostsByCompanyIdQuery, List<PostViewModel>>
    {
        private readonly IPostRepository _repository;
        private readonly IPostViewModelFactory _factory;

        public GetPostsByCompanyIdQueryHandler(IPostRepository repository, IPostViewModelFactory factory)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task<List<PostViewModel>> Handle(GetPostsByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            var posts = await _repository.GetPostsByCompanyId(request.CompanyId);

            foreach (var post in posts)
            {
                var sortedPostComments = post.GetSortedPostComments();


            }

            return posts.Select(p => _factory.CreateViewModel(p)).ToList();
        }
    }
}
