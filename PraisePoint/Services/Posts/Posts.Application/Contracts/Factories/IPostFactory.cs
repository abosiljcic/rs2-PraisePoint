using Posts.Application.Features.Posts.Commands.CreatePost;
using Posts.Application.Features.Posts.Queries.ViewModels;
using Posts.Domain.Aggregates;

namespace Posts.Application.Contracts.Factories;

public interface IPostFactory
{
    // Ovde imamo metod tipa
    // Order Create(CreateOrderCommand command);
    // Za svaki drugi Command koji imamo dodajemo novi metod npr.
    // Order Create(UpdateOrderCommand command);
    // Svaki agregat bi trebalo da ima svoju fabriku.
    Post Create(CreatePostCommand command);
}