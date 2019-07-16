using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizApp.Entities;
using QuizApp.Data.EntitiesConstraints;

namespace QuizApp.Data.Configuration
{
    public class TestResultConfiguration : IEntityTypeConfiguration<TestResult>
    {
        public void Configure(EntityTypeBuilder<TestResult> builder)
        {
            builder.ToTable(nameof(TestResult));

            builder.HasKey(tr => tr.Id);

            builder.Property(tr => tr.IntervieweeName).IsRequired().HasMaxLength(TestResultConstraints.IntervieweeNameMaxLength);

            builder.Property(tr => tr.PassingStartTime).HasColumnType("datetime2");

            builder.Property(tr => tr.PassingEndTime).HasColumnType("datetime2");

            builder.HasOne(tr => tr.Url).WithMany(u => u.TestResults).HasForeignKey(tr => tr.UrlId);

            builder.HasMany(tr => tr.ResultAnswers).WithOne(ra => ra.Result).HasForeignKey(ra => ra.ResultId);
        }
    }
}
