using EMS.Core.Context;
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
    public class GenericRepository<T> : IGenericRepository<T>
         where T : class
    {
        private readonly IDatabaseContext Context;
        private readonly DbSet<T> dbSet;

        public GenericRepository(IDatabaseContext context)
        {
            this.Context = context;
            dbSet = context.Set<T>();
        }

        public virtual EntityState Add(T entity)
        {

            return dbSet.Add(entity).State;
        }

        public virtual void AddRange(IEnumerable<T> entity)
        {
            this.dbSet.AddRange(entity);
        }

        public T Get<TKey>(TKey id)
        {
            return dbSet.Find(id);
        }

        public async Task<T> GetAsync<TKey>(TKey id)
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
            IQueryable<T> query = dbSet;
            foreach (var item in include.Split(','))
            {
                query = query.Include(item);
            }
            return query.Where(predicate);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        public IQueryable<T> GetAll(int page, int pageCount)
        {
            var pageSize = (page - 1) * pageCount;

            return dbSet.Skip(pageSize).Take(pageCount);
        }

        public IQueryable<T> GetAll(string include)
        {
            return dbSet.Include(include);
        }

        public IQueryable<T> RawSql(string query, params object[] parameters)
        {
            return dbSet.FromSqlRaw(query, parameters);
        }

        public IQueryable<T> GetAll(string include, string include2)
        {
            return dbSet.Include(include).Include(include2);
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Any(predicate);
        }

        public virtual EntityState SoftDelete(T entity)
        {
            entity.GetType().GetProperty("IsActive")?.SetValue(entity, false);
            entity.GetType().GetProperty("IsDeleted")?.SetValue(entity, true);
            return dbSet.Update(entity).State;
        }

        public virtual EntityState HardDelete(T entity)
        {
            var entity1 = this.dbSet.Remove(entity).State;
            Context.SaveChanges();
            return entity1;
        }
        public virtual EntityState Update(T entity)
        {
            return dbSet.Update(entity).State;
        }
        public virtual async Task<List<T>> FromSqlRaw(string query)
        {
            return await dbSet?.FromSqlRaw(query).AsNoTracking().ToListAsync();
        }





        public async Task<T> GetByIdAsync(long id) => await Context.Set<T>().FindAsync(id);

        public long GetMaxAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, long>> max) { try { return Context.Set<T>().Where(predicate).Max(max); } catch { return 0; } }

        public async Task<T> GetFirstAsyncNoTrack(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var result = Context.Set<T>().AsNoTracking().Where(predicate);
            if (includes != null) foreach (var includeExpression in includes) result = result.Include(includeExpression);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var result = Context.Set<T>().Where(predicate);
            if (includes != null) foreach (var includeExpression in includes) result = result.Include(includeExpression);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate,
        Expression<Func<T, object>> OrderBy = null,
        Expression<Func<T, object>> OrderByDesc = null,
        List<string> ThenIncludes = null,
        params Expression<Func<T, object>>[] includes)
        {
            var result = Context.Set<T>().Where(predicate);
            if (includes != null) foreach (var includeExpression in includes) result = result.Include(includeExpression);
            if (ThenIncludes != null) foreach (var includeExpression in ThenIncludes) result = result.Include(includeExpression);

            if (OrderBy != null) result = result.OrderBy(OrderBy);
            if (OrderByDesc != null) result = result.OrderByDescending(OrderByDesc);

            return await result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await Context.Set<T>().ToListAsync();
        //public async Task<IEnumerable<T>> StoredProcedures(
        //    Expression<Func<T, bool>> predicate,
        //    params Expression<Func<T, object>>[] includes)
        //{
        //    var result = Context.Set<T>().FromSql(predicate, params);
        //    return await Context.Set<T>().FromSql(predicate, includes);
        //}; // .ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsync(PagingData paging = null,
            Expression<Func<T, object>> OrderBy = null,
            Expression<Func<T, object>> OrderByDesc = null,
            List<string> ThenIncludes = null,
            params Expression<Func<T, object>>[] includes)
        {
            var result = Context.Set<T>().Where(i => true);
            if (includes != null) foreach (var includeExpression in includes) result = result.Include(includeExpression);
            if (ThenIncludes != null) foreach (var includeExpression in ThenIncludes) result = result.Include(includeExpression);

            if (OrderBy != null) result = result.OrderBy(OrderBy);
            if (OrderByDesc != null) result = result.OrderByDescending(OrderByDesc);

            if (paging != null) if (paging.IsPagingEnabled) result = result.Skip(paging.Skip).Take(paging.Take);

            return await result.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate) => await Context.Set<T>().Where(predicate).ToListAsync();

        public async Task<bool> GetAnyAsync(Expression<Func<T, bool>> predicate) => await Context.Set<T>().AnyAsync(predicate);

        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate,
            PagingData paging = null,
            Expression<Func<T, object>> OrderBy = null,
            Expression<Func<T, object>> OrderByDesc = null,
            List<string> ThenIncludes = null, params Expression<Func<T, object>>[] includes)
        {
            var result = Context.Set<T>() as IQueryable<T>;
            if (predicate != null) result = result.Where(predicate);
            if (includes != null) foreach (var includeExpression in includes) result = result.Include(includeExpression);
            if (ThenIncludes != null) foreach (var includeExpression in ThenIncludes) result = result.Include(includeExpression);

            if (OrderBy != null) result = result.OrderBy(OrderBy);
            if (OrderByDesc != null) result = result.OrderByDescending(OrderByDesc);

            if (paging != null) if (paging.IsPagingEnabled) result = result.Skip(paging.Skip).Take(paging.Take);

            return await result.ToListAsync();
        }

        public Tuple<IEnumerable<T>, long> GetPagingWhereAsync(Expression<Func<T, bool>> predicate,
            PagingData paging = null,
            Expression<Func<T, object>> OrderBy = null,
            Expression<Func<T, object>> OrderByDesc = null,
            List<string> ThenIncludes = null, params Expression<Func<T, object>>[] includes)
        {
            var result = Context.Set<T>().Where(predicate);
            if (includes != null) foreach (var includeExpression in includes) result = result.Include(includeExpression);
            if (ThenIncludes != null) foreach (var includeExpression in ThenIncludes) result = result.Include(includeExpression);

            if (OrderBy != null) result = result.OrderBy(OrderBy);
            if (OrderByDesc != null) result = result.OrderByDescending(OrderByDesc);

            var tempresult = result;

            if (paging != null) if (paging.IsPagingEnabled) result = result.Skip(paging.Skip).Take(paging.Take);

            return new Tuple<IEnumerable<T>, long>(result, tempresult.Count());
        }

        public Tuple<IEnumerable<T>, long> GetPagingWhereAsNoTrackingAsync(Expression<Func<T, bool>> predicate,
           PagingData paging = null,
           Expression<Func<T, object>> OrderBy = null,
           Expression<Func<T, object>> OrderByDesc = null,
           List<string> ThenIncludes = null, params Expression<Func<T, object>>[] includes)
        {
            var result = Context.Set<T>().AsNoTracking().Where(predicate);
            if (includes != null) foreach (var includeExpression in includes) result = result.Include(includeExpression);
            if (ThenIncludes != null) foreach (var includeExpression in ThenIncludes) result = result.Include(includeExpression);

            if (OrderBy != null) result = result.OrderBy(OrderBy);
            if (OrderByDesc != null) result = result.OrderByDescending(OrderByDesc);

            var tempresult = result;

            if (paging != null) if (paging.IsPagingEnabled) result = result.Skip(paging.Skip).Take(paging.Take);

            return new Tuple<IEnumerable<T>, long>(result, tempresult.Count());
        }

        public async Task<int> CountAllAsync() => await Context.Set<T>().CountAsync();

        public async Task<int> CountWhereAsync(Expression<Func<T, bool>> predicate) => await Context.Set<T>().CountAsync(predicate);

        public async Task<T> AddAsync(T entity)
        {
            Context.Set<T>().Add(entity);
            await Context.SaveChangesAsync(CancellationToken.None);
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity)
        {
            Context.Set<T>().AddRange(entity);
            await Context.SaveChangesAsync(CancellationToken.None);
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            //Context.Entry(entity).State = EntityState.Modified;
            dbSet.Update(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task UpdateAllAsync(IEnumerable<T> entity)
        {
            foreach (var item in entity)
            {
                //Context.Entry(item).State = EntityState.Modified;
                dbSet.Update(item).State = EntityState.Modified;
            }
            await Context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task RemoveAsync(T entity)
        {
            Context.Set<T>().Remove(entity);
            await Context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entity)
        {
            Context.Set<T>().RemoveRange(entity);
            await Context.SaveChangesAsync(CancellationToken.None);
        }

        public Task<IEnumerable<T>> GetAllDataAsync(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> OrderBy = null,
            Expression<Func<T, object>> OrderByDesc = null,
            List<string> ThenIncludes = null,
            params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }
    }
}
