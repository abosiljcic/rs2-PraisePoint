using Microsoft.EntityFrameworkCore;
using Posts.Application.Contracts.Persistence;
using Posts.Domain.Aggregates;

namespace Posts.Infrastructure.Persistence.Repositories;

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

    public async Task<IReadOnlyCollection<Post>> GetPostsByUsername(string username)
    {
        return await _dbContext.Set<Post>()
            .Where(post => post.SenderUsername == username)
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