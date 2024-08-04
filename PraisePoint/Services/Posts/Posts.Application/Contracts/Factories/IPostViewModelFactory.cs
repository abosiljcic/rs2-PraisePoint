using Posts.Application.Features.Posts.Queries.ViewModels;
using Posts.Domain.Aggregates;

namespace Posts.Application.Contracts.Factories;

public interface IPostViewModelFactory
{
    PostViewModel CreateViewModel(Post post);
}