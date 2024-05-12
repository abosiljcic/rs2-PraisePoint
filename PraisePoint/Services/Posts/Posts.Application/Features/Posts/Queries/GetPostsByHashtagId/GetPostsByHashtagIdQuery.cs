using MediatR;
using Posts.Application.Features.Posts.Queries.ViewModels;
using Posts.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Posts.Application.Features.Posts.Queries.GetPostsByHashtagId
{
    public class GetPostsByHashtagIdQuery : IRequest<List<PostViewModel>>
    {
        public Guid HashtagId { get; set; }

        public GetPostsByHashtagIdQuery(Guid hashtagId)
        {
            HashtagId = hashtagId;
        }
    }
}
