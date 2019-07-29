using Microsoft.EntityFrameworkCore;

using QuizApp.Data.Interfaces;
using QuizApp.Entities;

namespace QuizApp.Data.Repositories
{
    public class ResultAnswerOptionRepository : GenericRepository<ResultAnswerOption>, IResultAnswerOptionRepository
    {
        public ResultAnswerOptionRepository(DbContext dbContext) : base(dbContext) { }
    }
}
