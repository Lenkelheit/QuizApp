using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizApp.Entities;

namespace QuizApp.Data.Configuration
{
    public class TestResultConfiguration : IEntityTypeConfiguration<TestResult>
    {
        public void Configure(EntityTypeBuilder<TestResult> builder)
        {
            builder.Property(tr => tr.IntervieweeName).IsRequired().HasMaxLength(EntitiesConstraints.INTERVIEWEE_NAME_MAX_LENGTH);

            builder.Property(tr => tr.PassingStartTime).IsRequired().HasColumnType("datetime2");

            builder.Property(tr => tr.PassingEndTime).IsRequired().HasColumnType("datetime2");

            builder.HasOne(tr => tr.Url).WithMany(u => u.TestResults).HasForeignKey(tr => tr.UrlId);

            builder.HasMany(tr => tr.ResultAnswers).WithOne(ra => ra.Result).HasForeignKey(ra => ra.ResultId);
        }
    }
}
