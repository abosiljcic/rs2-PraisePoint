using MediatR;
using Posts.Application.Features.Posts.Queries.ViewModels;

namespace Posts.Application.Features.Posts.Queries.GetPostsByUsername;

public class GetPostsBySenderUsernameQuery : IRequest<List<PostViewModel>>
{
    public string SenderUsername { get; set; }

    public GetPostsBySenderUsernameQuery(string senderUsername)
    {
        SenderUsername = senderUsername ?? throw new ArgumentNullException(nameof(senderUsername));
    }
}