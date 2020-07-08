using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data.Repositories;
using Data;
using System.Threading;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
         where T : class
    {
        private readonly IAlbumsDBContext albumDBContext;
        private readonly DbSet<T> dbSet;
        private readonly CancellationToken cancellationToken;

        public GenericRepository(IAlbumsDBContext _albumDBContext)
        {
            this.albumDBContext = _albumDBContext;
            dbSet = albumDBContext.Set<T>();
        }

        public async virtual Task<T> Add(T entity)
        {
            dbSet.Add(entity);
            await albumDBContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<T> Get<TKey>(TKey id)
        {
            return await dbSet.FindAsync(id);
        }


        public T Get(params object[] keyValues)
        {
            return dbSet.Find(keyValues);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, string include)
        {
            return FindBy(predicate).Include(include);
        }

        public IQueryable<T> GetAll()
        {
            return dbSet;
        }

        public IQueryable<T> GetAll(int page, int pageCount)
        {
            var pageSize = (page - 1) * pageCount;

            return dbSet.Skip(pageSize).Take(pageCount);
        }

        public virtual EntityState Update(T entity)
        {
            return dbSet.Update(entity).State;
        }
    }
}
