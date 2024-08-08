using MediatR;
using Posts.Application.Contracts.Factories;
using Posts.Application.Contracts.Persistence;
using Posts.Application.Features.Posts.Queries.ViewModels;

namespace Posts.Application.Features.Posts.Queries.GetPostsByUsername;

public class GetPostsByUsernameQueryHandler : IRequestHandler<GetPostsBySenderUsernameQuery, List<PostViewModel>>
{
    private readonly IPostRepository _repository;
    private readonly IPostViewModelFactory _factory;

    public GetPostsByUsernameQueryHandler(IPostRepository repository, IPostViewModelFactory factory)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    public async Task<List<PostViewModel>> Handle(GetPostsBySenderUsernameQuery request, CancellationToken cancellationToken)
    {
        var posts = await _repository.GetPostsBySenderUsername(request.SenderUsername);
        return posts.Select(post => _factory.CreateViewModel(post)).ToList();
    }
}