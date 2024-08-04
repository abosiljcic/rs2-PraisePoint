using MediatR;
using Posts.Application.Contracts.Factories;
using Posts.Application.Contracts.Persistence;
using Posts.Application.Features.Posts.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Posts.Application.Features.Posts.Queries.GetPostById
{
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostViewModel>
    {
        private readonly IPostRepository _repository;
        private readonly IPostViewModelFactory _factory;

        public GetPostByIdQueryHandler(IPostRepository repository, IPostViewModelFactory factory)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task<PostViewModel> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _repository.GetPostById(request.Id);
            return _factory.CreateViewModel(post);
        }
    }
}
