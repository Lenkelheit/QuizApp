using System.Collections.Generic;

using QuizApp.Entities;

namespace QuizApp.Data.Interfaces
{
    public interface ITestEventRepository : IRepository<TestEvent>
    {
        void Delete(IEnumerable<TestEvent> testEvents);
    }
}
