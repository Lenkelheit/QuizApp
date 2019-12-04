using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using QuizApp.Data.Interfaces;
using QuizApp.Entities;

namespace QuizApp.Data.Repositories
{
    public class ResultAnswerRepository : GenericRepository<ResultAnswer>, IResultAnswerRepository
    {
        public ResultAnswerRepository(DbContext dbContext) : base(dbContext) { }


        public IEnumerable<ResultAnswer> GetPageWithAmount(Expression<Func<ResultAnswer, bool>> filter = null,
                                                           int page = 0,
                                                           int amountAnswersPerPage = 3,
                                                           Func<IQueryable<ResultAnswer>, IIncludableQueryable<ResultAnswer, object>> includeProperties = null)
        {
            IQueryable<ResultAnswer> query = DbSet;

            if (filter != null) query = query.Where(filter);

            query = query.Skip(page * amountAnswersPerPage).Take(amountAnswersPerPage);

            if (includeProperties != null) query = includeProperties(query);

            return query;
        }
    }
}
