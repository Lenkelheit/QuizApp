using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

using QuizApp.Entities;

namespace QuizApp.Data.Interfaces
{
    public interface IResultAnswerRepository : IRepository<ResultAnswer>
    {
        IEnumerable<ResultAnswer> GetPageWithAmount(Expression<Func<ResultAnswer, bool>> filter = null,
                                                    int page = 0,
                                                    int amountAnswersPerPage = 3,
                                                    Func<IQueryable<ResultAnswer>, IIncludableQueryable<ResultAnswer, object>> includeProperties = null);
    }
}
