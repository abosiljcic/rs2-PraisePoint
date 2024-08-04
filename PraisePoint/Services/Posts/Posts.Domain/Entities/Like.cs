using Posts.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Posts.Domain.Entities
{
    public class Like : EntityBase
    {
        public string Username { get; private set; }
        public Guid PostId { get; set; }

        public Like() { }

        public Like(string username, Guid postId)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
            PostId = postId;
        }
    }
}
