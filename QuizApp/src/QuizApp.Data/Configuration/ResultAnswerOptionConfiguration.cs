using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizApp.Entities;

namespace QuizApp.Data.Configuration
{
    public class ResultAnswerOptionConfiguration : IEntityTypeConfiguration<ResultAnswerOption>
    {
        public void Configure(EntityTypeBuilder<ResultAnswerOption> builder)
        {
            builder.HasOne(raopt => raopt.Option).WithMany(tqopt => tqopt.ResultAnswerOptions).HasForeignKey(raopt => raopt.OptionId).OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(raopt => raopt.ResultAnswer).WithMany(ra => ra.ResultAnswerOptions);
        }
    }
}
