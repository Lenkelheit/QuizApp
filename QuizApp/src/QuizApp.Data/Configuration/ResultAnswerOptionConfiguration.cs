using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizApp.Entities;

namespace QuizApp.Data.Configuration
{
    public class ResultAnswerOptionConfiguration : IEntityTypeConfiguration<ResultAnswerOption>
    {
        public void Configure(EntityTypeBuilder<ResultAnswerOption> builder)
        {
            builder.ToTable(nameof(ResultAnswerOption)).HasKey(opt => opt.Id);

            builder.HasOne(ropt => ropt.Option).WithMany(topt => topt.ResultAnswerOptions).HasForeignKey(ropt => ropt.OptionId).OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(opt => opt.ResultAnswer).WithMany(ra => ra.ResultAnswerOptions).HasForeignKey(opt => opt.ResultAnswerId);
        }
    }
}
