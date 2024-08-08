using MediatR;
using Posts.Application.Features.Posts.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Posts.Application.Features.Posts.Queries.GetPostsByReceiverUsername
{
    public class GetPostsByReceiverUsernameQuery : IRequest<List<PostViewModel>>
    {
        public string ReceiverUsername { get; set; }

        public GetPostsByReceiverUsernameQuery(string receiverUsername)
        {
            ReceiverUsername = receiverUsername ?? throw new ArgumentNullException(nameof(receiverUsername));
        }
    }
}
