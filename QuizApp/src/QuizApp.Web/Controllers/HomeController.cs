using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

using QuizApp.Entities;
using QuizApp.Data.Context;

namespace QuizApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private QuizAppDbContext quizAppDbContext;

        public HomeController(QuizAppDbContext quizAppDbContext)
        {
            this.quizAppDbContext = quizAppDbContext;

            ClearDatabaseTables();
            FillDatabase();
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<object>> Get()
        {
            return quizAppDbContext.Users.ToArray();
        }

        [HttpGet("deluser")]
        public ActionResult<IEnumerable<object>> DeleteUser()
        {
            ClearDatabaseTables();
            FillDatabase();

            User user1 = quizAppDbContext.Users.FirstOrDefault();

            quizAppDbContext.Users.Remove(user1);
            quizAppDbContext.SaveChanges();

            return new object[] { user1, quizAppDbContext.Users.ToArray(), quizAppDbContext.Tests.ToArray() };
        }

        [HttpGet("deltest")]
        public ActionResult<IEnumerable<object>> DeleteTest()
        {
            ClearDatabaseTables();
            FillDatabase();

            Test test1 = quizAppDbContext.Tests.FirstOrDefault();

            quizAppDbContext.Tests.Remove(test1);
            quizAppDbContext.SaveChanges();

            return new object[] { test1, quizAppDbContext.Tests.ToArray(), quizAppDbContext.Urls.ToArray(), quizAppDbContext.TestQuestions.ToArray() };
        }

        [HttpGet("delquestion")]
        public ActionResult<IEnumerable<object>> DeleteQuestion()
        {
            ClearDatabaseTables();
            FillDatabase();

            TestQuestion testQuestion1 = quizAppDbContext.TestQuestions.FirstOrDefault();

            quizAppDbContext.TestQuestions.Remove(testQuestion1);
            quizAppDbContext.SaveChanges();

            return new object[] { testQuestion1, quizAppDbContext.TestQuestions.ToArray(), quizAppDbContext.ResultAnswers.ToArray(), quizAppDbContext.TestQuestionOptions.ToArray() };
        }

        [HttpGet("delquestionopt")]
        public ActionResult<IEnumerable<object>> DeleteQuestionOption()
        {
            ClearDatabaseTables();
            FillDatabase();
            
            TestQuestionOption testQuestionOption1 = quizAppDbContext.TestQuestionOptions.FirstOrDefault();

            quizAppDbContext.TestQuestionOptions.Remove(testQuestionOption1);
            quizAppDbContext.SaveChanges();

            return new object[] { testQuestionOption1, quizAppDbContext.TestQuestionOptions.ToArray(), quizAppDbContext.ResultAnswerOptions.ToArray() };
        }

        [HttpGet("delurl")]
        public ActionResult<IEnumerable<object>> DeleteUrl()
        {
            ClearDatabaseTables();
            FillDatabase();

            Url url1 = quizAppDbContext.Urls.FirstOrDefault();

            quizAppDbContext.Urls.Remove(url1);
            quizAppDbContext.SaveChanges();

            return new object[] { url1, quizAppDbContext.Urls.ToArray(), quizAppDbContext.TestResults.ToArray() };
        }

        [HttpGet("delresult")]
        public ActionResult<IEnumerable<object>> DeleteResult()
        {
            ClearDatabaseTables();
            FillDatabase();

            TestResult testResult1 = quizAppDbContext.TestResults.FirstOrDefault();

            quizAppDbContext.TestResults.Remove(testResult1);
            quizAppDbContext.SaveChanges();

            return new object[] { testResult1, quizAppDbContext.TestResults.ToArray(), quizAppDbContext.ResultAnswers.ToArray() };
        }

        [HttpGet("delresanswer")]
        public ActionResult<IEnumerable<object>> DeleteResultAnswer()
        {
            ClearDatabaseTables();
            FillDatabase();

            ResultAnswer resultAnswer1 = quizAppDbContext.ResultAnswers.FirstOrDefault();

            quizAppDbContext.ResultAnswers.Remove(resultAnswer1);
            quizAppDbContext.SaveChanges();

            return new object[] { resultAnswer1, quizAppDbContext.ResultAnswers.ToArray(), quizAppDbContext.ResultAnswerOptions.ToArray() };
        }

        [HttpGet("delansweropt")]
        public ActionResult<IEnumerable<object>> DeleteResultAnswerOption()
        {
            ClearDatabaseTables();
            FillDatabase();

            ResultAnswerOption resultAnswerOption1 = quizAppDbContext.ResultAnswerOptions.FirstOrDefault();

            quizAppDbContext.ResultAnswerOptions.Remove(resultAnswerOption1);
            quizAppDbContext.SaveChanges();

            return new object[] { resultAnswerOption1, quizAppDbContext.ResultAnswerOptions.ToArray() };
        }


        private void FillDatabase()
        {
            User user1 = new User { Username = "Sam", Email = "sam@gmail.com", Password = "1111" };
            User user2 = new User { Username = "Mike", Email = "Mike@gmail.com", Password = "1111" };
            quizAppDbContext.Users.AddRange(user1, user2);

            Test test1 = new Test { Title = "First", Description = "Some first desciption", Author = user1 };
            Test test2 = new Test { Title = "Second", Description = "Some second desciption", Author = user2 };
            quizAppDbContext.Tests.AddRange(test1, test2);

            TestQuestion testQuestion1 = new TestQuestion { Text = "Some first question", Hint = "Some hint", Test = test1 };
            TestQuestion testQuestion2 = new TestQuestion { Text = "Some second question", Hint = "Some hint", Test = test1 };
            TestQuestion testQuestion3 = new TestQuestion { Text = "Some question", Hint = "Some hint", Test = test2 };
            quizAppDbContext.TestQuestions.AddRange(testQuestion1, testQuestion2, testQuestion3);

            TestQuestionOption testQuestionOption1 = new TestQuestionOption { Text = "Some text1", IsRight = false, Question = testQuestion1 };
            TestQuestionOption testQuestionOption2 = new TestQuestionOption { Text = "Some text2", IsRight = true, Question = testQuestion1 };
            TestQuestionOption testQuestionOption3 = new TestQuestionOption { Text = "Some text3", IsRight = false, Question = testQuestion1 };

            TestQuestionOption testQuestionOption4 = new TestQuestionOption { Text = "Some text4", IsRight = true, Question = testQuestion2 };
            TestQuestionOption testQuestionOption5 = new TestQuestionOption { Text = "Some text5", IsRight = false, Question = testQuestion2 };

            TestQuestionOption testQuestionOption6 = new TestQuestionOption { Text = "Some text6", IsRight = false, Question = testQuestion3 };
            TestQuestionOption testQuestionOption7 = new TestQuestionOption { Text = "Some text7", IsRight = true, Question = testQuestion3 };
            quizAppDbContext.TestQuestionOptions.AddRange(testQuestionOption1, testQuestionOption2, testQuestionOption3, testQuestionOption4,
                testQuestionOption5, testQuestionOption6, testQuestionOption7);

            Url url1 = new Url { IntervieweeName = "Ann", Test = test1 };
            Url url2 = new Url { IntervieweeName = "Kate", Test = test2 };
            quizAppDbContext.Urls.AddRange(url1, url2);

            TestResult testResult1 = new TestResult { IntervieweeName = "Ann", Score = 75, Url = url1 };
            TestResult testResult2 = new TestResult { IntervieweeName = "Kate", Score = 60, Url = url2 };
            quizAppDbContext.TestResults.AddRange(testResult1, testResult2);

            ResultAnswer resultAnswer1 = new ResultAnswer { TimeTakenSeconds = new TimeSpan(0, 3, 30), Question = testQuestion1, Result = testResult1 };
            ResultAnswer resultAnswer2 = new ResultAnswer { TimeTakenSeconds = new TimeSpan(0, 4, 23), Question = testQuestion2, Result = testResult1 };
            ResultAnswer resultAnswer3 = new ResultAnswer { TimeTakenSeconds = new TimeSpan(0, 1, 57), Question = testQuestion3, Result = testResult2 };
            quizAppDbContext.ResultAnswers.AddRange(resultAnswer1, resultAnswer2, resultAnswer3);

            ResultAnswerOption resultAnswerOption1 = new ResultAnswerOption { ResultAnswer = resultAnswer1, Option = testQuestionOption1 };
            ResultAnswerOption resultAnswerOption2 = new ResultAnswerOption { ResultAnswer = resultAnswer2, Option = testQuestionOption4 };
            ResultAnswerOption resultAnswerOption3 = new ResultAnswerOption { ResultAnswer = resultAnswer3, Option = testQuestionOption6 };
            quizAppDbContext.ResultAnswerOptions.AddRange(resultAnswerOption1, resultAnswerOption2, resultAnswerOption3);

            quizAppDbContext.SaveChanges();
        }

        private void ClearDatabaseTables()
        {
            quizAppDbContext.Users.RemoveRange(quizAppDbContext.Users);
            quizAppDbContext.Tests.RemoveRange(quizAppDbContext.Tests);
            quizAppDbContext.Urls.RemoveRange(quizAppDbContext.Urls);
            quizAppDbContext.TestResults.RemoveRange(quizAppDbContext.TestResults);
            quizAppDbContext.ResultAnswers.RemoveRange(quizAppDbContext.ResultAnswers);
            quizAppDbContext.ResultAnswerOptions.RemoveRange(quizAppDbContext.ResultAnswerOptions);
            quizAppDbContext.TestQuestions.RemoveRange(quizAppDbContext.TestQuestions);
            quizAppDbContext.TestQuestionOptions.RemoveRange(quizAppDbContext.TestQuestionOptions);

            quizAppDbContext.SaveChanges();
        }
    }
}