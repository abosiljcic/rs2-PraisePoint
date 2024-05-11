using Post.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Contracts.Persistence
{
    public interface IPostRepository : IAsyncRepository<Domain.Aggregates.Post>
    {
        Task<IReadOnlyCollection<Domain.Aggregates.Post>> GetPostsByCompanyId(Guid companyId);
        Task<IReadOnlyCollection<Domain.Aggregates.Post>> GetPostsByUsername(string username);
        Task<IReadOnlyCollection<Domain.Aggregates.Post>> GetPostsByHashtagId(Guid hashtagId);
        Task<Domain.Aggregates.Post> GetPostById(Guid id);
    }
}
