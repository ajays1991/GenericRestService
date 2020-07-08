using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
//using Data.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Add(T entity);
       
        EntityState Update(T entity);
        
        Task<T> Get<TKey>(TKey id);

        T Get(params object[] keyValues);

        /// <returns>Entity</returns>
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        /// <returns>Queryable</returns>
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, string include);

        /// <returns>List of entities</returns>
        IQueryable<T> GetAll();

        /// <returns>Queryable</returns>
        IQueryable<T> GetAll(int page, int pageCount);
    }
}