using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizApp.Entities;
using QuizApp.Data.EntitiesConstraints;

namespace QuizApp.Data.Configuration
{
    public class UrlConfiguration : IEntityTypeConfiguration<Url>
    {
        public void Configure(EntityTypeBuilder<Url> builder)
        {
            builder.ToTable(nameof(Url));

            builder.HasKey(u => u.Id);

            builder.Property(u => u.ValidFromTime).HasColumnType("datetime2");

            builder.Property(u => u.ValidUntilTime).HasColumnType("datetime2");

            builder.Property(u => u.IntervieweeName).HasMaxLength(UrlConstraints.IntervieweeNameMaxLength);

            builder.HasOne(u => u.Test).WithMany(t => t.Urls).HasForeignKey(u => u.TestId);

            builder.HasMany(u => u.TestResults).WithOne(tr => tr.Url).HasForeignKey(tr => tr.UrlId);
        }
    }
}
