using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QuizApp.Data.Interfaces
{
    public interface IRepository<TEntity>
    {
        int Count(Expression<Func<TEntity, bool>> predicate = null);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                 string includeProperties = "");

        TEntity GetById(object id, string includeProperties = "");

        Task<TEntity> GetByIdAsync(object id, string includeProperties = "");

        void Insert(TEntity entity);

        Task InsertAsync(TEntity entity);

        void Update(TEntity entityToUpdate);

        void Delete(TEntity entityToDelete);

        void Delete(Expression<Func<TEntity, bool>> predicate);
    }
}
