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
        Task<IReadOnlyCollection<Post>> GetPostsBySenderUsername(string senderUsername);
        Task<IReadOnlyCollection<Post>> GetPostsByReceiverUsername(string receiverUsername);

        Task<IReadOnlyCollection<Post>> GetPostsByHashtagId(Guid hashtagId);
        Task<Post?> GetPostById(Guid id);
    }
}
