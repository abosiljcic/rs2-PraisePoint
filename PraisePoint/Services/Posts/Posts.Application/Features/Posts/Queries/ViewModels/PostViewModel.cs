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
        public string ImageUrl { get; set; }

        public IEnumerable<CommentViewModel> PostComments { get; set; }
}