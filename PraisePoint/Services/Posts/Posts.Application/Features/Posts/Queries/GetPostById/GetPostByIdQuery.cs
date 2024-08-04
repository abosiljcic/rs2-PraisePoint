using MediatR;
using Posts.Application.Features.Posts.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Posts.Application.Features.Posts.Queries.GetPostById
{
    public class GetPostByIdQuery : IRequest<PostViewModel>
    {
        public Guid Id { get; set; }

        public GetPostByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
