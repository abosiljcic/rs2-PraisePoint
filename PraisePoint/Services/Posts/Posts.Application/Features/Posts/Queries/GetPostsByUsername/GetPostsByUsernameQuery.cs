namespace Posts.Application.Features.Posts.Queries.GetPostsByUsername;

public class GetPostsByUsernameQuery
{
    public string Username { get; set; }

    public GetPostsByUsernameQuery(string username)
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
    }
}