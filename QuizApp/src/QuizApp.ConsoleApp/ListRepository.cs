using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

using QuizApp.Data.Interfaces;

namespace QuizApp.ConsoleApp
{
    public class ListRepository<T> : IRepository<T> where T : class
    {
        public List<T> List { get; } = new List<T>();

        public int Count(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? List.Count : List.Count(predicate.Compile());
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
                                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties = null)
        {
            IQueryable<T> query = List.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null) query = orderBy(query);

            return query;
        }

        public T GetById(object id, Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties = null)
        {
            return List[(int)id];
        }

        public async Task<T> GetByIdAsync(object id, Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties = null)
        {
            return await Task.Run(() => List[(int)id]);
        }

        public void Insert(T entity)
        {
            List.Add(entity);
        }

        public async Task InsertAsync(T entity)
        {
            await Task.Run(() => List.Add(entity));
        }

        public void Update(T entityToUpdate)
        {
            throw new NotSupportedException();
        }

        public void Delete(T entityToDelete)
        {
            List.Remove(entityToDelete);
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            if (predicate != null) List.RemoveAll(elem => predicate.Compile().Invoke(elem));
            else List.Clear();
        }
    }
}
