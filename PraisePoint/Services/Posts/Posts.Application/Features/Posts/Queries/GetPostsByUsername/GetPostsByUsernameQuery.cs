using MediatR;
using Posts.Application.Features.Posts.Queries.ViewModels;

namespace Posts.Application.Features.Posts.Queries.GetPostsByUsername;

public class GetPostsByUsernameQuery : IRequest<List<PostViewModel>>
{
    public string Username { get; set; }

    public GetPostsByUsernameQuery(string username)
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
    }
}