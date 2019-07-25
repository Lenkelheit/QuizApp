using Microsoft.EntityFrameworkCore;

using QuizApp.Data.Interfaces;
using QuizApp.Entities;

namespace QuizApp.Data.Repositories
{
    public class TestQuestionOptionRepository : GenericRepository<TestQuestionOption>, ITestQuestionOptionRepository
    {
        public TestQuestionOptionRepository(DbContext dbContext) : base(dbContext) { }
    }
}
