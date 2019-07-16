using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizApp.Entities;
using QuizApp.Data.EntitiesConstraints;

namespace QuizApp.Data.Configuration
{
    public class TestQuestionConfiguration : IEntityTypeConfiguration<TestQuestion>
    {
        public void Configure(EntityTypeBuilder<TestQuestion> builder)
        {
            builder.ToTable(nameof(TestQuestion));

            builder.HasKey(tq => tq.Id);

            builder.Property(tq => tq.Text).IsRequired().HasMaxLength(TestQuestionConstraints.TextQuestionMaxLength);

            builder.Property(tq => tq.Hint).HasMaxLength(TestQuestionConstraints.HintMaxLength);

            builder.Property(tq => tq.TimeLimitSeconds).HasColumnType("time");

            builder.HasOne(tq => tq.Test).WithMany(t => t.TestQuestions).HasForeignKey(tq => tq.TestId);

            builder.HasMany(tq => tq.TestQuestionOptions).WithOne(tqopt => tqopt.Question).HasForeignKey(tqopt => tqopt.QuestionId);

            builder.HasMany(tq => tq.ResultAnswers).WithOne(ra => ra.Question).HasForeignKey(ra => ra.QuestionId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
