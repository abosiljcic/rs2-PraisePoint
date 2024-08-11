using Posts.Application.Features.Posts.Commands.AddComment;
using Posts.Application.Features.Posts.Commands.AddLikeCommand;
using Posts.Application.Features.Posts.Commands.CreatePost;
using Posts.Domain.Aggregates;
using Posts.Domain.Entities;

namespace Posts.Application.Contracts.Factories;

public interface IPostFactory
{
    // Ovde imamo metod tipa
    // Order Create(CreateOrderCommand command);
    // Za svaki drugi Command koji imamo dodajemo novi metod npr.
    // Order Create(UpdateOrderCommand command);
    // Svaki agregat bi trebalo da ima svoju fabriku.
    Post Create(CreatePostCommand command);
    Comment Create(AddCommentCommand command);
    Like Create(ToggleLikeCommand command);
}