using Microsoft.EntityFrameworkCore;

using QuizApp.Data.Interfaces;
using QuizApp.Entities;

namespace QuizApp.Data.Repositories
{
    public class TestQuestionRepository : GenericRepository<TestQuestion>, ITestQuestionRepository
    {
        public TestQuestionRepository(DbContext dbContext) : base(dbContext) { }
    }
}
