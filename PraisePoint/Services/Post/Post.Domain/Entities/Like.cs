using Post.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Entities
{
    public class Like : EntityBase
    {
        public string Username { get; private set; }

        public Like() { }

        public Like(string username)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
        }
    }
}
