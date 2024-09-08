using Posts.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Posts.Domain.Entities
{
    public class Comment : EntityBase
    {
        public string Username { get; private set; }
        public string Text { get; private set; }

        public Comment() { }

        public Comment(string username, string text)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Text = text ?? throw new ArgumentNullException(nameof(text));
            CreatedDate = DateTime.Now;
        }
    }
}
