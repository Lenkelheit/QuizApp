using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizApp.Entities;

namespace QuizApp.Data.Configuration
{
    public class TestQuestionConfiguration : IEntityTypeConfiguration<TestQuestion>
    {
        public void Configure(EntityTypeBuilder<TestQuestion> builder)
        {
            builder.Property(tq => tq.Text).IsRequired();

            builder.Property(tq => tq.TimeLimitSeconds).IsRequired().HasColumnType("time");

            builder.HasOne(tq => tq.Test).WithMany(t => t.TestQuestions);

            builder.HasMany(tq => tq.TestQuestionOptions).WithOne(tq => tq.Question);

            builder.HasMany(tq => tq.ResultAnswers).WithOne(ra => ra.Question).HasForeignKey(ra => ra.QuestionId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
