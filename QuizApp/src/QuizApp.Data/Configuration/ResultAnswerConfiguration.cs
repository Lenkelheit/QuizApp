using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizApp.Entities;

namespace QuizApp.Data.Configuration
{
    public class ResultAnswerConfiguration : IEntityTypeConfiguration<ResultAnswer>
    {
        public void Configure(EntityTypeBuilder<ResultAnswer> builder)
        {
            builder.Property(ra => ra.TakeTimeSeconds).IsRequired().HasColumnType("time");

            builder.HasOne(ra => ra.Result).WithMany(tr => tr.ResultAnswers);

            builder.HasOne(ra => ra.Question).WithMany(q => q.ResultAnswers).HasForeignKey(ra => ra.QuestionId).OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(ra => ra.ResultAnswerOptions).WithOne(raopt => raopt.ResultAnswer);
        }
    }
}
