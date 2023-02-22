using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Context
{
    public interface IDatabaseContext
    {
        /// <returns>set for the specified entity</returns>
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
