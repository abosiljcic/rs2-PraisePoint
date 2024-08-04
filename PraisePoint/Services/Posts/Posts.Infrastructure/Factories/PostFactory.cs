using Posts.Application.Contracts.Factories;
using Posts.Application.Features.Posts.Commands.CreatePost;
using Posts.Domain.Aggregates;

namespace Posts.Infrastructure.Factories;

public class PostFactory: IPostFactory
{
    public Post Create(CreatePostCommand command)
    {
        var post = new Post(command.SenderUsername, command.ReceiverUsername, command.CompanyId, command.Points,
            command.Description);
        return post;
    }
}