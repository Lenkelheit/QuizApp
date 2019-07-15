using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizApp.Entities;

namespace QuizApp.Data.Configuration
{
    public class TestQuestionConfiguration : IEntityTypeConfiguration<TestQuestion>
    {
        public void Configure(EntityTypeBuilder<TestQuestion> builder)
        {
            builder.Property(tq => tq.Text).IsRequired().HasMaxLength(EntitiesConstraints.TEXT_QUESTION_MAX_LENGTH);

            builder.Property(tq => tq.Hint).HasMaxLength(EntitiesConstraints.HINT_MAX_LENGTH);

            builder.Property(tq => tq.TimeLimitSeconds).IsRequired().HasColumnType("time");

            builder.HasOne(tq => tq.Test).WithMany(t => t.TestQuestions).HasForeignKey(tq => tq.TestId);

            builder.HasMany(tq => tq.TestQuestionOptions).WithOne(tqopt => tqopt.Question).HasForeignKey(tqopt => tqopt.QuestionId);

            builder.HasMany(tq => tq.ResultAnswers).WithOne(ra => ra.Question).HasForeignKey(ra => ra.QuestionId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
