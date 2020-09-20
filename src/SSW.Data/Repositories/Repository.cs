using SSW.Data.Contexts;
using SSW.Data.Entities;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SSW.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly UniversityDbContext _context;

        public Repository(UniversityDbContext context)
        {
            _context = context;
        }

        public Task<TEntity> FirstOrDefaultAsync(int id)
        {
            return _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<TResult> FirstOrDefaultAsync<TResult>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TResult>> selector)
        {
            return _context.Set<TEntity>().Where(where).Select(selector).FirstOrDefaultAsync();
        }

        public async Task<ICollection<TEntity>> ToListAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<ICollection<TResult>> ToListAsync<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            return await _context.Set<TEntity>().Select(selector).ToListAsync();
        }

        public async Task<ICollection<TResult>> ToListAsync<TResult>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TResult>> selector)
        {
            return await _context.Set<TEntity>().Where(where).Select(selector).ToListAsync();
        }
    }
}
