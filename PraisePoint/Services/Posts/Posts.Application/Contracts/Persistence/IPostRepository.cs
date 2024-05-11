using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Posts.Domain.Aggregates;

namespace Posts.Application.Contracts.Persistence
{
    public interface IPostRepository : IAsyncRepository<Post>
    {
        Task<IReadOnlyCollection<Post>> GetPostsByCompanyId(Guid companyId);
        Task<IReadOnlyCollection<Post>> GetPostsByUsername(string username);
        Task<IReadOnlyCollection<Post>> GetPostsByHashtagId(Guid hashtagId);
        Task<Post> GetPostById(Guid id);
    }
}
