using Microsoft.EntityFrameworkCore;
using QuizApp.Entities;

namespace QuizApp.Data
{
    public class DataBaseContext : DbContext
    {
        // CONSTRUCTORS
        public DataBaseContext()
        {
            Database.EnsureCreated();
        }

        // PROPERTIES
        public DbSet<User> Users { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Url> Urls { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<ResultAnswer> ResultAnswers { get; set; }
        public DbSet<ResultAnswerOption> ResultAnswerOptions { get; set; }
        public DbSet<TestQuestion> TestQuestions { get; set; }
        public DbSet<TestQuestionOption> TestQuestionOptions { get; set; }

        // METHODS
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=QuizAppDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entities

            // Configure User
            modelBuilder.Entity<User>().Property(u => u.Username).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired();
            modelBuilder.Entity<User>().HasMany(u => u.Tests).WithOne(t => t.Author);

            // Configure Test
            modelBuilder.Entity<Test>().Property(t => t.Title).IsRequired();
            modelBuilder.Entity<Test>().Property(t => t.TimeLimitSeconds).IsRequired().HasColumnType("time");
            modelBuilder.Entity<Test>().Property(t => t.LastModifiedDate).IsRequired().HasColumnType("datetime2");
            modelBuilder.Entity<Test>().HasMany(t => t.Urls).WithOne(u => u.Test);
            modelBuilder.Entity<Test>().HasMany(t => t.TestQuestions).WithOne(q => q.Test);

            // Configure Url
            modelBuilder.Entity<Url>().Property(u => u.ValidFromTime).IsRequired().HasColumnType("datetime2");
            modelBuilder.Entity<Url>().Property(u => u.ValidUntilTime).IsRequired().HasColumnType("datetime2");
            modelBuilder.Entity<Url>().HasMany(u => u.TestResults).WithOne(tr => tr.Url);

            // Configure TestResult
            modelBuilder.Entity<TestResult>().Property(tr => tr.IntervieweeName).IsRequired();
            modelBuilder.Entity<TestResult>().Property(tr => tr.PassingStartTime).IsRequired().HasColumnType("datetime2");
            modelBuilder.Entity<TestResult>().Property(tr => tr.PassingEndTime).IsRequired().HasColumnType("datetime2");
            modelBuilder.Entity<TestResult>().HasMany(tr => tr.ResultAnswers).WithOne(tr => tr.Result);

            // Configure ResultAnswer
            modelBuilder.Entity<ResultAnswer>().Property(ra => ra.TakeTimeSeconds).IsRequired().HasColumnType("time");
            modelBuilder.Entity<ResultAnswer>().HasOne(ra => ra.Question).WithMany(q => q.ResultAnswers).HasForeignKey(ra => ra.QuestionId).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<ResultAnswer>().HasMany(ra => ra.ResultAnswerOptions).WithOne(raopt => raopt.ResultAnswer);

            // Configure ResultAnswerOption
            modelBuilder.Entity<ResultAnswerOption>().HasOne(raopt => raopt.Option).WithMany(tqopt => tqopt.ResultAnswerOptions).HasForeignKey(raopt => raopt.OptionId).OnDelete(DeleteBehavior.SetNull);

            // Configure TestQuestion
            modelBuilder.Entity<TestQuestion>().Property(tq => tq.Text).IsRequired();
            modelBuilder.Entity<TestQuestion>().Property(tq => tq.TimeLimitSeconds).IsRequired().HasColumnType("time");
            modelBuilder.Entity<TestQuestion>().HasMany(tq => tq.TestQuestionOptions).WithOne(tq => tq.Question);

            // Configure TestQuestionOption
            modelBuilder.Entity<TestQuestionOption>().Property(tqopt => tqopt.Text).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
