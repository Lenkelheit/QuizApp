using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using QuizApp.Data.Interfaces;
using QuizApp.Entities;

namespace QuizApp.Data.Repositories
{
    public class TestRepository : GenericRepository<Test>, ITestRepository
    {
        public TestRepository(DbContext dbContext) : base(dbContext) { }


        public override void Update(Test test)
        {
            base.Update(test);

            IQueryable<TestQuestion> missingQuestions = DbContext.Set<TestQuestion>().Where(question => question.TestId == test.Id).Except(test.TestQuestions);
            DbContext.RemoveRange(missingQuestions);

            IEnumerable<int> questionsId = test.TestQuestions.Select(question => question.Id);
            IEnumerable<int> questionOptionsId = test.TestQuestions.SelectMany(question => question.TestQuestionOptions.Select(option => option.Id));
            IQueryable<TestQuestionOption> missingQuestionOptions = DbContext.Set<TestQuestionOption>()
                .Where(option => questionsId.Contains(option.QuestionId) && !questionOptionsId.Contains(option.Id));

            DbContext.RemoveRange(missingQuestionOptions);
        }
    }
}
