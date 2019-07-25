using Microsoft.EntityFrameworkCore;

using QuizApp.Data.Interfaces;
using QuizApp.Entities;

namespace QuizApp.Data.Repositories
{
    public class ResultAnswerRepository : GenericRepository<ResultAnswer>, IResultAnswerRepository
    {
        public ResultAnswerRepository(DbContext dbContext) : base(dbContext) { }
    }
}
