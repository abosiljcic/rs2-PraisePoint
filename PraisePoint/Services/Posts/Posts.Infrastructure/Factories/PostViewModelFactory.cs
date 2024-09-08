using Posts.Application.Contracts.Factories;
using Posts.Application.Features.Posts.Queries.ViewModels;
using Posts.Domain.Aggregates;

namespace Posts.Infrastructure.Factories;

public class PostViewModelFactory: IPostViewModelFactory
{
    public PostViewModel CreateViewModel(Post post)
    {
        var postViewModel = new PostViewModel();
        postViewModel.Id = post.Id;
        postViewModel.SenderUsername = post.SenderUsername;
        postViewModel.ReceiverUsername = post.ReceiverUsername;
        postViewModel.CompanyId = post.CompanyId;
        postViewModel.Description = post.Description;
        postViewModel.Points = post.Points;
        postViewModel.CreatedDate = post.CreatedDate;

        var postComments = new List<CommentViewModel>();
        foreach (var comment in post.PostComments)
        {
            var commentItem = new CommentViewModel();
            commentItem.Id = comment.Id;
            commentItem.Username = comment.Username;
            commentItem.Text = comment.Text;
            commentItem.CreatedDate = comment.CreatedDate;
            postComments.Add(commentItem);
        }
        
        var postLikes = new List<LikeViewModel>();
        foreach (var like in post.PostLikes)
        {
            var likeItem = new LikeViewModel();
            likeItem.Id = like.Id;
            likeItem.Username = like.Username;
            postLikes.Add(likeItem);
        }

        postViewModel.PostComments = postComments;
        postViewModel.PostLikes = postLikes;
        postViewModel.NumberOfLikes = postLikes.Count;
        postViewModel.NumberOfComments = postComments.Count;



        return postViewModel;
    }
}