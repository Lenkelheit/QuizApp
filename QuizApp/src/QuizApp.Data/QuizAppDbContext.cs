using Microsoft.EntityFrameworkCore;
using QuizApp.Entities;

namespace QuizApp.Data
{
    public class QuizAppDbContext : DbContext
    {
        public QuizAppDbContext(DbContextOptions<QuizAppDbContext> options) : base(options) { }


        public DbSet<User> Users { get; set; }

        public DbSet<Test> Tests { get; set; }

        public DbSet<Url> Urls { get; set; }

        public DbSet<TestResult> TestResults { get; set; }

        public DbSet<ResultAnswer> ResultAnswers { get; set; }

        public DbSet<ResultAnswerOption> ResultAnswerOptions { get; set; }

        public DbSet<TestQuestion> TestQuestions { get; set; }

        public DbSet<TestQuestionOption> TestQuestionOptions { get; set; }
    }
}
