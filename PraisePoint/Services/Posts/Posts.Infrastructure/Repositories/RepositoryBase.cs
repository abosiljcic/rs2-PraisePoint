using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Posts.Application.Contracts.Persistence;
using Posts.Domain.Common;
using Posts.Infrastructure.Persistence;

namespace Posts.Infrastructure.Repositories;

public class RepositoryBase<T>: IAsyncRepository<T> where T: AggregateRoot
{
    protected readonly PostContext _dbContext;

    public RepositoryBase(PostContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public virtual async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public virtual async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbContext.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
        string? includeString = null, 
        bool disableTracking = true)
    {
        IQueryable<T> query = _dbContext.Set<T>();
        if (disableTracking) 
            query = query.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(includeString)) 
            query = query.Include(includeString);

        query = query.Where(predicate);

        return await orderBy(query).ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy, 
        List<Expression<Func<T, object>>> includes, 
        bool disableTracking = true)
    {
        IQueryable<T> query = _dbContext.Set<T>();
        if (disableTracking) 
            query = query.AsNoTracking();

        query = includes
            .Aggregate(query, (current, include) => current.Include(include))
            .Where(predicate);

        return orderBy is null 
            ? await query.ToListAsync()
            : await orderBy(query).ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        _dbContext.Set<T>().Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }  
}