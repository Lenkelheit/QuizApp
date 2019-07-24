using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using QuizApp.Data.Interfaces;

namespace QuizApp.Data.Repositories
{
    public abstract class GenericRepository<TEntity> : IDbContextSettable, IRepository<TEntity> where TEntity : class
    {
        protected DbContext dbContext;

        protected DbSet<TEntity> dbSet;


        public GenericRepository()
        {
            this.dbContext = null;
            this.dbSet = null;
        }

        public GenericRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }


        public void SetDbContext(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            ContextCheck();

            return predicate == null ? dbSet.Count() : dbSet.Count(predicate);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                        string includeProperties = "")
        {
            ContextCheck();

            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (string includeProperty in includeProperties.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null) query = orderBy(query);

            return query;
        }

        public TEntity GetById(object id, string includeProperties = "")
        {
            ContextCheck();

            IQueryable<TEntity> query = dbSet;

            foreach (string includeProperty in includeProperties.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            dbSet = query as DbSet<TEntity>;

            return dbSet?.Find(id);
        }

        public async Task<TEntity> GetByIdAsync(object id, string includeProperties = "")
        {
            ContextCheck();

            IQueryable<TEntity> query = dbSet;

            foreach (string includeProperty in includeProperties.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            dbSet = query as DbSet<TEntity>;

            return await dbSet?.FindAsync(id);
        }

        public void Insert(TEntity entity)
        {
            ContextCheck();
            dbSet.Add(entity);
        }

        public async Task InsertAsync(TEntity entity)
        {
            ContextCheck();

            await dbSet.AddAsync(entity);
        }

        public void Update(TEntity entityToUpdate)
        {
            if (dbContext.Entry(entityToUpdate).State == EntityState.Detached)
            {
                dbSet.Attach(entityToUpdate);
            }
            dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void Delete(TEntity entityToDelete)
        {
            ContextCheck();
            if (entityToDelete == null) throw new ArgumentNullException(nameof(entityToDelete));

            if (dbContext.Entry(entityToDelete).State == EntityState.Detached) 
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            ContextCheck();

            if (predicate != null) dbSet.RemoveRange(dbSet.Where(predicate));
            else dbSet.RemoveRange(dbSet);
        }

        protected void ContextCheck()
        {
            if (dbSet == null) throw new NullReferenceException("Database context is not setted.");
        }
    }
}
