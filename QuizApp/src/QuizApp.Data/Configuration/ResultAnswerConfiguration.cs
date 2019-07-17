using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizApp.Entities;

namespace QuizApp.Data.Configuration
{
    public class ResultAnswerConfiguration : IEntityTypeConfiguration<ResultAnswer>
    {
        public void Configure(EntityTypeBuilder<ResultAnswer> builder)
        {
            builder.ToTable(nameof(ResultAnswer)).HasKey(ra => ra.Id);

            builder.Property(ra => ra.TimeTakenSeconds).HasColumnType("time");

            builder.HasOne(ra => ra.Result).WithMany(tr => tr.ResultAnswers).HasForeignKey(ra => ra.ResultId);

            builder.HasOne(ra => ra.Question).WithMany(q => q.ResultAnswers).HasForeignKey(ra => ra.QuestionId).OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(ra => ra.ResultAnswerOptions).WithOne(opt => opt.ResultAnswer).HasForeignKey(opt => opt.ResultAnswerId);
        }
    }
}
