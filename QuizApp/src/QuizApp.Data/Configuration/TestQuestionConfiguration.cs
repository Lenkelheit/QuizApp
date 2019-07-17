using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizApp.Entities;

namespace QuizApp.Data.Configuration
{
    public class TestQuestionConfiguration : IEntityTypeConfiguration<TestQuestion>
    {
        public void Configure(EntityTypeBuilder<TestQuestion> builder)
        {
            builder.ToTable(nameof(TestQuestion)).HasKey(tq => tq.Id);

            builder.Property(tq => tq.Text).IsRequired().HasMaxLength(512);

            builder.Property(tq => tq.Hint).HasMaxLength(256);

            builder.Property(tq => tq.TimeLimitSeconds).HasColumnType("time");

            builder.HasOne(tq => tq.Test).WithMany(t => t.TestQuestions).HasForeignKey(tq => tq.TestId);

            builder.HasMany(tq => tq.TestQuestionOptions).WithOne(opt => opt.Question).HasForeignKey(opt => opt.QuestionId);

            builder.HasMany(tq => tq.ResultAnswers).WithOne(ra => ra.Question).HasForeignKey(ra => ra.QuestionId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
