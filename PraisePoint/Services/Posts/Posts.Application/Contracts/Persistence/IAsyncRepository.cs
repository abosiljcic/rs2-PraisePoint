﻿using Post.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : AggregateRoot
    {
        Task<IReadOnlyList<T>> GetAllAsync(); 
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetAsync(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
        string? includeString = null,
        bool disableTracking = true);
        Task<IReadOnlyList<T>> GetAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
            List<Expression<Func<T, object>>> includes,
            bool disableTracking = true);
        Task<T?> GetByIdAsync(Guid id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
