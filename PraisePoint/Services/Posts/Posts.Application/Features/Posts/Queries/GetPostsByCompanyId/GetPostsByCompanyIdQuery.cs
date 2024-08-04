using MediatR;
using Posts.Application.Features.Posts.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Posts.Application.Features.Posts.Queries.GetPostsByCompanyId
{
    public class GetPostsByCompanyIdQuery : IRequest<List<PostViewModel>>
    {
        public Guid CompanyId { get; set; }

        public GetPostsByCompanyIdQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}
