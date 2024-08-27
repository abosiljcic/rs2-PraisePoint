using Posts.Domain.Entities;

namespace Posts.Application.Contracts.Persistence;

public interface ICommentRepository : IAsyncRepository<Comment>
{
    
}