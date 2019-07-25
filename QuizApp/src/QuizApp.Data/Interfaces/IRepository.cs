using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace QuizApp.Data.Interfaces
{
    public interface IRepository<TEntity>
    {
        int Count(Expression<Func<TEntity, bool>> predicate = null);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                 Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeProperties = null);

        TEntity GetById(object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeProperties = null);

        Task<TEntity> GetByIdAsync(object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeProperties = null);

        void Insert(TEntity entity);

        Task InsertAsync(TEntity entity);

        void Update(TEntity entityToUpdate);

        void Delete(TEntity entityToDelete);

        void Delete(Expression<Func<TEntity, bool>> predicate);
    }
}
