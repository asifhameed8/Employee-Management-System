using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using EMS.Core.Context;
using EMS.Core.Repositories;
using EMS.Core.Factory;

namespace EMS.Service.Uow
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private IDatabaseContext dbContext;
        private readonly DatabaseContext _dBConnectionContext;

        private Dictionary<Type, object> repos;

        public UnitOfWork(IContextFactory contextFactory, DatabaseContext dBConnectionContext)
        {
            dbContext = contextFactory.DbContext;
            _dBConnectionContext = dBConnectionContext;
        }
        public DatabaseContext Tables()
        {
            return _dBConnectionContext;
        }
        public List<TEntity> SpRepository<TEntity>(string SpName, params object[] parameters) where TEntity : class
        {
            return _dBConnectionContext.Set<TEntity>().FromSqlRaw(SpName, parameters).ToList<TEntity>();
        }
        public IGenericRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            if (repos == null)
            {
                repos = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!repos.ContainsKey(type))
            {
                repos[type] = new GenericRepository<TEntity>(dbContext);
            }

            return (IGenericRepository<TEntity>)repos[type];
        }
            [Obsolete]
        public DatabaseFacade Database()
        {
            try
            {
                return _dBConnectionContext.Database;
            }
            finally
            {
                this.repos = null;
            }
        }
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        public long Commit()
        {
            return dbContext.SaveChanges();
        }
        public async Task<long> CommitAsync()
        {
            return await dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(obj: this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dbContext != null)
                {
                    dbContext.Dispose();
                    dbContext = null;
                }
            }
        }
    }
}
