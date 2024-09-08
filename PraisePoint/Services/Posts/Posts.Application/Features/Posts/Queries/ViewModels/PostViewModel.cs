using Posts.Domain.Entities;

namespace Posts.Application.Features.Posts.Queries.ViewModels;

public class PostViewModel
{ 
        public Guid Id { get; set; }
        
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }
        public Guid CompanyId { get; set; }
        public int Points { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public IEnumerable<CommentViewModel> PostComments { get; set; }
        
        public IEnumerable<LikeViewModel> PostLikes { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfComments { get; set; }

}