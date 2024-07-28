using MediatR;
using Posts.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Posts.Application.Features.Posts.Commands.CreatePost
{
    public class CreatePostCommand : IRequest<Guid>
    {
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }
        public Guid CompanyId { get; set; }
        public int Points { get; set; }
        public string Description { get; set; }

    }
}
