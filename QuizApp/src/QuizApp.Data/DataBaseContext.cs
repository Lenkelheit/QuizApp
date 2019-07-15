using Microsoft.EntityFrameworkCore;
using QuizApp.Entities;

namespace QuizApp.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            Database.Migrate();
        }


        public DbSet<User> Users { get; set; }

        public DbSet<Test> Tests { get; set; }

        public DbSet<Url> Urls { get; set; }

        public DbSet<TestResult> TestResults { get; set; }

        public DbSet<ResultAnswer> ResultAnswers { get; set; }

        public DbSet<ResultAnswerOption> ResultAnswerOptions { get; set; }

        public DbSet<TestQuestion> TestQuestions { get; set; }

        public DbSet<TestQuestionOption> TestQuestionOptions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entities

            modelBuilder.Entity<User>().Property(u => u.Username).IsRequired().HasMaxLength(EntitiesConstraints.USERNAME_MAX_LENGTH);
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(EntitiesConstraints.EMAIL_MAX_LENGTH);
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired().HasMaxLength(EntitiesConstraints.PASSWORD_MAX_LENGTH);
            modelBuilder.Entity<User>().HasMany(u => u.CreatedTests).WithOne(t => t.Author).HasForeignKey(t => t.AuthorId);

            modelBuilder.Entity<Test>().Property(t => t.Title).IsRequired().HasMaxLength(EntitiesConstraints.TITLE_MAX_LENGTH);
            modelBuilder.Entity<Test>().Property(t => t.Description).HasMaxLength(EntitiesConstraints.DESCRIPTION_MAX_LENGTH);
            modelBuilder.Entity<Test>().Property(t => t.TimeLimitSeconds).IsRequired().HasColumnType("time");
            modelBuilder.Entity<Test>().Property(t => t.LastModifiedDate).IsRequired().HasColumnType("datetime2");
            modelBuilder.Entity<Test>().HasMany(t => t.Urls).WithOne(u => u.Test).HasForeignKey(u => u.TestId);
            modelBuilder.Entity<Test>().HasMany(t => t.TestQuestions).WithOne(q => q.Test).HasForeignKey(q => q.TestId);

            modelBuilder.Entity<Url>().Property(u => u.ValidFromTime).IsRequired().HasColumnType("datetime2");
            modelBuilder.Entity<Url>().Property(u => u.ValidUntilTime).IsRequired().HasColumnType("datetime2");
            modelBuilder.Entity<Url>().HasMany(u => u.TestResults).WithOne(tr => tr.Url).HasForeignKey(tr => tr.UrlId);

            modelBuilder.Entity<TestResult>().Property(tr => tr.IntervieweeName).IsRequired().HasMaxLength(EntitiesConstraints.INTERVIEWEE_NAME_MAX_LENGTH);
            modelBuilder.Entity<TestResult>().Property(tr => tr.PassingStartTime).IsRequired().HasColumnType("datetime2");
            modelBuilder.Entity<TestResult>().Property(tr => tr.PassingEndTime).IsRequired().HasColumnType("datetime2");
            modelBuilder.Entity<TestResult>().HasMany(tr => tr.ResultAnswers).WithOne(ra => ra.Result).HasForeignKey(ra => ra.ResultId);

            modelBuilder.Entity<ResultAnswer>().Property(ra => ra.TimeTakenSeconds).IsRequired().HasColumnType("time");
            modelBuilder.Entity<ResultAnswer>().HasOne(ra => ra.Question).WithMany(q => q.ResultAnswers).HasForeignKey(ra => ra.QuestionId).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<ResultAnswer>().HasMany(ra => ra.ResultAnswerOptions).WithOne(raopt => raopt.ResultAnswer).HasForeignKey(raopt => raopt.ResultAnswerId);

            modelBuilder.Entity<ResultAnswerOption>().HasOne(raopt => raopt.Option).WithMany(tqopt => tqopt.ResultAnswerOptions).HasForeignKey(raopt => raopt.OptionId).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TestQuestion>().Property(tq => tq.Text).IsRequired().HasMaxLength(EntitiesConstraints.TEXT_QUESTION_MAX_LENGTH);
            modelBuilder.Entity<TestQuestion>().Property(tq => tq.Hint).HasMaxLength(EntitiesConstraints.HINT_MAX_LENGTH);
            modelBuilder.Entity<TestQuestion>().Property(tq => tq.TimeLimitSeconds).IsRequired().HasColumnType("time");
            modelBuilder.Entity<TestQuestion>().HasMany(tq => tq.TestQuestionOptions).WithOne(tqopt => tqopt.Question).HasForeignKey(tqopt => tqopt.QuestionId);

            modelBuilder.Entity<TestQuestionOption>().Property(tqopt => tqopt.Text).IsRequired().HasMaxLength(EntitiesConstraints.TEXT_QUESTION_OPTION_MAX_LENGTH);

            base.OnModelCreating(modelBuilder);
        }
    }
}
