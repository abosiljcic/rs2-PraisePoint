using Posts.Application.Contracts.Factories;
using Posts.Application.Features.Posts.Commands.AddComment;
using Posts.Application.Features.Posts.Commands.AddLikeCommand;
using Posts.Application.Features.Posts.Commands.CreatePost;
using Posts.Domain.Aggregates;
using Posts.Domain.Entities;

namespace Posts.Infrastructure.Factories;

public class PostFactory: IPostFactory
{
    public Post Create(CreatePostCommand command)
    {
        var post = new Post(command.SenderUsername, command.ReceiverUsername, command.CompanyId, command.Points,
            command.Description);
        return post;
    }

    public Comment Create(AddCommentCommand command)
    {
        var comment = new Comment(command.Username, command.Text);
        return comment;
    }
    
    public Like Create(ToggleLikeCommand command)
    {
        var like = new Like(command.Username);
        return like;
    }
}