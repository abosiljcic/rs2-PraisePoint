namespace Posts.Application.Features.Posts.Queries.ViewModels;

public class LikeViewModel
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public Guid PostId { get; set; }
}