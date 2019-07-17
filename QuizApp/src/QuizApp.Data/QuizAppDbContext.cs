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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(nameof(User)).HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.Username).IsRequired().HasMaxLength(128);
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(128);
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired().HasMaxLength(256);
            modelBuilder.Entity<User>().HasMany(u => u.CreatedTests).WithOne(t => t.Author).HasForeignKey(t => t.AuthorId);

            modelBuilder.Entity<Test>().ToTable(nameof(Test)).HasKey(t => t.Id);
            modelBuilder.Entity<Test>().Property(t => t.Title).IsRequired().HasMaxLength(128);
            modelBuilder.Entity<Test>().Property(t => t.Description).HasMaxLength(512);
            modelBuilder.Entity<Test>().Property(t => t.TimeLimitSeconds).HasColumnType("time");
            modelBuilder.Entity<Test>().Property(t => t.LastModifiedDate).HasColumnType("datetime2");
            modelBuilder.Entity<Test>().HasMany(t => t.Urls).WithOne(u => u.Test).HasForeignKey(u => u.TestId);
            modelBuilder.Entity<Test>().HasMany(t => t.TestQuestions).WithOne(q => q.Test).HasForeignKey(q => q.TestId);

            modelBuilder.Entity<Url>().ToTable(nameof(Url)).HasKey(u => u.Id);
            modelBuilder.Entity<Url>().Property(u => u.ValidFromTime).HasColumnType("datetime2");
            modelBuilder.Entity<Url>().Property(u => u.ValidUntilTime).HasColumnType("datetime2");
            modelBuilder.Entity<Url>().Property(u => u.IntervieweeName).HasMaxLength(128);
            modelBuilder.Entity<Url>().HasMany(u => u.TestResults).WithOne(tr => tr.Url).HasForeignKey(tr => tr.UrlId);

            modelBuilder.Entity<TestResult>().ToTable(nameof(TestResult)).HasKey(tr => tr.Id);
            modelBuilder.Entity<TestResult>().Property(tr => tr.IntervieweeName).IsRequired().HasMaxLength(128);
            modelBuilder.Entity<TestResult>().Property(tr => tr.PassingStartTime).HasColumnType("datetime2");
            modelBuilder.Entity<TestResult>().Property(tr => tr.PassingEndTime).HasColumnType("datetime2");
            modelBuilder.Entity<TestResult>().HasMany(tr => tr.ResultAnswers).WithOne(ra => ra.Result).HasForeignKey(ra => ra.ResultId);

            modelBuilder.Entity<ResultAnswer>().ToTable(nameof(ResultAnswer)).HasKey(ra => ra.Id);
            modelBuilder.Entity<ResultAnswer>().Property(ra => ra.TimeTakenSeconds).HasColumnType("time");
            modelBuilder.Entity<ResultAnswer>().HasOne(ra => ra.Question).WithMany(q => q.ResultAnswers).HasForeignKey(ra => ra.QuestionId).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<ResultAnswer>().HasMany(ra => ra.ResultAnswerOptions).WithOne(opt => opt.ResultAnswer).HasForeignKey(opt => opt.ResultAnswerId);

            modelBuilder.Entity<ResultAnswerOption>().ToTable(nameof(ResultAnswerOption)).HasKey(ropt => ropt.Id);
            modelBuilder.Entity<ResultAnswerOption>().HasOne(ropt => ropt.Option)
                                                     .WithMany(topt => topt.ResultAnswerOptions)
                                                     .HasForeignKey(ropt => ropt.OptionId).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TestQuestion>().ToTable(nameof(TestQuestion)).HasKey(tq => tq.Id);
            modelBuilder.Entity<TestQuestion>().Property(tq => tq.Text).IsRequired().HasMaxLength(512);
            modelBuilder.Entity<TestQuestion>().Property(tq => tq.Hint).HasMaxLength(256);
            modelBuilder.Entity<TestQuestion>().Property(tq => tq.TimeLimitSeconds).HasColumnType("time");
            modelBuilder.Entity<TestQuestion>().HasMany(tq => tq.TestQuestionOptions).WithOne(opt => opt.Question).HasForeignKey(opt => opt.QuestionId);

            modelBuilder.Entity<TestQuestionOption>().ToTable(nameof(TestQuestionOption)).HasKey(opt => opt.Id);
            modelBuilder.Entity<TestQuestionOption>().Property(opt => opt.Text).IsRequired().HasMaxLength(256);

            base.OnModelCreating(modelBuilder);
        }
    }
}
