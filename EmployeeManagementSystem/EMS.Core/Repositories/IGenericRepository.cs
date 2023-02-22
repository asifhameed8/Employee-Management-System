using EMS.Models.Command;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Repositories
{
    public interface IGenericRepository<T>
    {
        /// <returns>The Entity's state</returns>
        EntityState Add(T entity);
        void AddRange(IEnumerable<T> entity);
        /// <returns>The Entity's state</returns>
        EntityState Update(T entity);
        /// <returns>Entity</returns>
        T Get<TKey>(TKey id);

        /// <returns>Task Entity</returns>
        Task<T> GetAsync<TKey>(TKey id);

        /// <returns>The requested Entity</returns>
        T Get(params object[] keyValues);

        /// <returns>Entity</returns>
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);


        /// <returns>Queryable</returns>
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, string include);

        /// <returns>List of entities</returns>
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);

        /// <returns>Queryable</returns>
        IQueryable<T> GetAll(int page, int pageCount);

        /// <returns>List of entities</returns>
        IQueryable<T> GetAll(string include);

        /// <returns>List of entities</returns>
        IQueryable<T> RawSql(string sql, params object[] parameters);

        /// <returns>List of entities</returns>
        IQueryable<T> GetAll(string include, string include2);

        /// <summary>
        /// Soft delete with using IsActive flag for entity
        /// </summary>
        /// <returns>The Entity's state</returns>
        EntityState SoftDelete(T entity);

        /// <summary>
        /// Deletes the specified entity
        /// </summary>
        /// <returns>The Entity's state</returns>
        EntityState HardDelete(T entity);

        bool Exists(Expression<Func<T, bool>> predicate);

        Task<List<T>> FromSqlRaw(string query);






        Task<T> GetByIdAsync(long id);
        Task<T> GetFirstAsyncNoTrack(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> OrderBy = null,
            Expression<Func<T, object>> OrderByDesc = null,
            List<string> ThenIncludes = null,
            params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate);
        Task<bool> GetAnyAsync(Expression<Func<T, bool>> predicate);

        long GetMaxAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, long>> max);

        Task<IEnumerable<T>> GetAllAsync(PagingData paging = null,
            Expression<Func<T, object>> OrderBy = null,
            Expression<Func<T, object>> OrderByDesc = null,
            List<string> ThenIncludes = null,
            params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> GetAllDataAsync(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> OrderBy = null,
            Expression<Func<T, object>> OrderByDesc = null,
            List<string> ThenIncludes = null,
            params Expression<Func<T, object>>[] includes);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes">add comma saperated includes</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate,
            PagingData paging = null,
            Expression<Func<T, object>> OrderBy = null,
            Expression<Func<T, object>> OrderByDesc = null,
            List<string> ThenIncludes = null,
            params Expression<Func<T, object>>[] includes);

        Tuple<IEnumerable<T>, long> GetPagingWhereAsync(Expression<Func<T, bool>> predicate,
            PagingData paging = null,
            Expression<Func<T, object>> OrderBy = null,
            Expression<Func<T, object>> OrderByDesc = null,
            List<string> ThenIncludes = null,
            params Expression<Func<T, object>>[] includes);

        Tuple<IEnumerable<T>, long> GetPagingWhereAsNoTrackingAsync(Expression<Func<T, bool>> predicate,
            PagingData paging = null,
            Expression<Func<T, object>> OrderBy = null,
            Expression<Func<T, object>> OrderByDesc = null,
            List<string> ThenIncludes = null,
            params Expression<Func<T, object>>[] includes);

        Task<int> CountAllAsync();
        Task<int> CountWhereAsync(Expression<Func<T, bool>> predicate);

        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity);
        Task UpdateAsync(T entity);
        Task UpdateAllAsync(IEnumerable<T> entity);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entity);
    }
}
