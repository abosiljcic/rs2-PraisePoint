using Post.Domain.Common;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Aggregates
{
    public class Post : AggregateRoot
    {
        public string SenderUsername { get; private set; }
        public string ReceiverUsername { get; private set; }
        public Guid CompanyId { get; private set; }
        public int Points { get; private set; }
        public string Description { get; private set; }
        public string ImageUrl { get; private set; }

        private readonly List<Like> _postLikes = new List<Like>();
        public IReadOnlyCollection<Like> PostLikes => _postLikes;

        private readonly List<Comment> _postComments = new List<Comment>();

        public IReadOnlyCollection<Comment> PostComments => _postComments;


        public Post(string senderUsername, string receiverUsername, Guid companyId, int points, string description, string imageUrl)
        {
            SenderUsername = senderUsername ?? throw new ArgumentNullException(nameof(senderUsername));
            ReceiverUsername = receiverUsername ?? throw new ArgumentNullException(nameof(receiverUsername));
            CompanyId = companyId;
            Points = points;
            Description = description ?? throw new ArgumentNullException(nameof(description));
            ImageUrl = imageUrl ?? throw new ArgumentNullException(nameof(imageUrl));
        }

        public Post(Guid id, string senderUsername, string receiverUsername, Guid companyId, int points, string description, string imageUrl): this(senderUsername, receiverUsername, companyId, points, description, imageUrl)
        {
            Id = id;
        }


        public Post(Guid id)
        {
            Id = id;
        }

        public void AddLike(string username)
        {
            var existingLikeForPost = PostLikes.SingleOrDefault(p => p.Username == username);
            if (existingLikeForPost is null)
            {
                var like = new Like(username);
                _postLikes.Add(like);
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
