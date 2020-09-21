using SSW.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SSW.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> FirstOrDefaultAsync(int id);
        Task<TResult> FirstOrDefaultAsync<TResult>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TResult>> selector);
        Task<ICollection<TEntity>> ToListAsync();
        Task<ICollection<TResult>> ToListAsync<TResult>(Expression<Func<TEntity, TResult>> selector);
        Task<ICollection<TResult>> ToListAsync<TResult>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TResult>> selector);
        Task<bool> Exist(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}