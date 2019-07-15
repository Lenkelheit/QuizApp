using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizApp.Entities;

namespace QuizApp.Data.Configuration
{
    public class UrlConfiguration : IEntityTypeConfiguration<Url>
    {
        public void Configure(EntityTypeBuilder<Url> builder)
        {
            builder.Property(u => u.ValidFromTime).IsRequired().HasColumnType("datetime2");

            builder.Property(u => u.ValidUntilTime).IsRequired().HasColumnType("datetime2");

            builder.Property(u => u.IntervieweeName).HasMaxLength(EntitiesConstraints.INTERVIEWEE_NAME_MAX_LENGTH);

            builder.HasOne(u => u.Test).WithMany(t => t.Urls).HasForeignKey(u => u.TestId);

            builder.HasMany(u => u.TestResults).WithOne(tr => tr.Url).HasForeignKey(tr => tr.UrlId);
        }
    }
}
