using Microsoft.EntityFrameworkCore;

using QuizApp.Data.Interfaces;
using QuizApp.Entities;

namespace QuizApp.Data.Repositories
{
    public class TestResultRepository : GenericRepository<TestResult>, ITestResultRepository
    {
        public TestResultRepository(DbContext dbContext) : base(dbContext) { }
    }
}
