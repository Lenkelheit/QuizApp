using QuizApp.Data.Context;
using QuizApp.Data.Interfaces;
using QuizApp.Entities;

namespace QuizApp.Data.Repositories
{
    public class TestQuestionRepository : GenericRepository<TestQuestion>, ITestQuestionRepository
    {
    }
}
