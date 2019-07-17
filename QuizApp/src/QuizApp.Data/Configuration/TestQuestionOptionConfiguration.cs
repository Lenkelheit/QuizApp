using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizApp.Entities;

namespace QuizApp.Data.Configuration
{
    public class TestQuestionOptionConfiguration : IEntityTypeConfiguration<TestQuestionOption>
    {
        public void Configure(EntityTypeBuilder<TestQuestionOption> builder)
        {
            builder.ToTable(nameof(TestQuestionOption)).HasKey(opt => opt.Id);

            builder.Property(opt => opt.Text).IsRequired().HasMaxLength(256);

            builder.HasOne(opt => opt.Question).WithMany(q => q.TestQuestionOptions).HasForeignKey(opt => opt.QuestionId);

            builder.HasMany(topt => topt.ResultAnswerOptions).WithOne(ropt => ropt.Option).HasForeignKey(ropt => ropt.OptionId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
