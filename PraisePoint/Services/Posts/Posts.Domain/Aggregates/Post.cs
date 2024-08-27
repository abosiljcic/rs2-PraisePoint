using Posts.Domain.Common;
using Posts.Domain.Entities;

namespace Posts.Domain.Aggregates
{
    public class Post : AggregateRoot
    {
        public string SenderUsername { get; private set; }
        public string ReceiverUsername { get; private set; }
        public Guid CompanyId { get; private set; }
        public int Points { get; private set; }
        public string Description { get; private set; }

        private readonly List<Like> _postLikes = new List<Like>();
        public IReadOnlyCollection<Like> PostLikes => _postLikes;

        private readonly List<Comment> _postComments = new List<Comment>();
        public IReadOnlyCollection<Comment> PostComments => _postComments;


        public Post(string senderUsername, string receiverUsername, Guid companyId, int points, string description)
        {
            SenderUsername = senderUsername ?? throw new ArgumentNullException(nameof(senderUsername));
            ReceiverUsername = receiverUsername ?? throw new ArgumentNullException(nameof(receiverUsername));
            CompanyId = companyId;
            Points = points;
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        public Post(Guid id, string senderUsername, string receiverUsername, Guid companyId, int points, string description): this(senderUsername, receiverUsername, companyId, points, description)
        {
            Id = id;
        }


        public Post(Guid id)
        {
            Id = id;
        }

        public void ToggleLiked(string username)
        {
            var like = new Like(username);
            var existingLikeForPost = PostLikes.SingleOrDefault(p => p.Username == username);
            if (existingLikeForPost is null)
            {
                _postLikes.Add(like);
            }
            else
            {
                _postLikes.Remove(existingLikeForPost);
            }
        }

        public void AddComment(string username, string text)
        {
            
             var comment = new Comment(username, text);
             _postComments.Add(comment);
            
        }
        public decimal GetTotalLikes() => PostLikes.Count();

        public decimal GetTotalComments() => PostComments.Count();
    }
}
