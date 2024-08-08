using Microsoft.EntityFrameworkCore;
using Posts.Application.Contracts.Persistence;
using Posts.Domain.Aggregates;
using Posts.Infrastructure.Persistence;

namespace Posts.Infrastructure.Repositories;

public class PostRepository: RepositoryBase<Post>, IPostRepository
{
    public PostRepository(PostContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<Post>> GetPostsByCompanyId(Guid companyId)
    {
        return await _dbContext.Set<Post>()
            .Where(post => post.CompanyId == companyId)
            .Include(post => post.PostLikes)
            .Include(post => post.PostComments)
            .ToListAsync(); 
    }

    public async Task<IReadOnlyCollection<Post>> GetPostsBySenderUsername(string senderUsername)
    {
        return await _dbContext.Set<Post>()
            .Where(post => post.SenderUsername == senderUsername)
            .Include(post => post.PostLikes)
            .Include(post => post.PostComments)
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<Post>> GetPostsByReceiverUsername(string receiverUsername)
    {
        return await _dbContext.Set<Post>()
            .Where(post => post.ReceiverUsername == receiverUsername)
            .Include(post => post.PostLikes)
            .Include(post => post.PostComments)
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<Post>> GetPostsByHashtagId(Guid hashtagId)
    {
        throw new NotImplementedException();
    }

    public async Task<Post?> GetPostById(Guid id)
    {
        return await _dbContext.Set<Post>()
            .Where(post => post.Id == id)
            .Include(post => post.PostLikes)
            .Include(post => post.PostComments)
            .SingleOrDefaultAsync();
    }

}