using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using QuizApp.Data.Interfaces;

namespace QuizApp.Data.Repositories
{
    public abstract class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext DbContext { get; set; }

        protected DbSet<TEntity> DbSet => DbContext?.Set<TEntity>();


        public GenericRepository(DbContext dbContext)
        {
            this.DbContext = dbContext ?? throw new NullReferenceException("Database context is not setted.");
        }


        public virtual int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? DbSet.Count() : DbSet.Count(predicate);
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeProperties = null)
        {
            IQueryable<TEntity> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null) query = includeProperties(query);

            if (orderBy != null) query = orderBy(query);

            return query;
        }

        public virtual TEntity GetById(object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeProperties = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (includeProperties != null) query = includeProperties(query);

            DbSet<TEntity> dbSet = query as DbSet<TEntity>;

            return dbSet?.Find(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeProperties = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (includeProperties != null) query = includeProperties(query);

            DbSet<TEntity> dbSet = query as DbSet<TEntity>;

            return await dbSet?.FindAsync(id);
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            if (DbContext.Entry(entityToUpdate).State == EntityState.Detached)
            {
                DbSet.Attach(entityToUpdate);
            }
            DbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (entityToDelete == null) throw new ArgumentNullException(nameof(entityToDelete));

            if (DbContext.Entry(entityToDelete).State == EntityState.Detached) 
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate != null) DbSet.RemoveRange(DbSet.Where(predicate));
            else DbSet.RemoveRange(DbSet);
        }
    }
}
