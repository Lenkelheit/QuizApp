using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizApp.Entities;

namespace QuizApp.Data.Configuration
{
    public class TestQuestionOptionConfiguration : IEntityTypeConfiguration<TestQuestionOption>
    {
        public void Configure(EntityTypeBuilder<TestQuestionOption> builder)
        {
            builder.Property(tqopt => tqopt.Text).IsRequired();

            builder.HasOne(tqopt => tqopt.Question).WithMany(q => q.TestQuestionOptions).HasForeignKey(tqopt => tqopt.QuestionId);

            builder.HasMany(tqopt => tqopt.ResultAnswerOptions).WithOne(raopt => raopt.Option).HasForeignKey(raopt => raopt.OptionId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
