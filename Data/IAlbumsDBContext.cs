using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Data.entities;

namespace Data
{
    public interface IAlbumsDBContext
    {
        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;

        /// <returns>number of state entries interacted with database</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        /// <returns>number of state entries interacted with database</returns>
        Task<int> SaveChangesAsync(bool confirmAllTransactions, CancellationToken cancellationToken);

        /// <returns>number of state entries interacted with database</returns>
        int SaveChanges();

        /// <returns>number of state entries interacted with database</returns>
        int SaveChanges(bool confirmAllTransactions);
        void Dispose();
    }
}

