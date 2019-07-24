using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizApp.Entities;

namespace QuizApp.Data.Configuration
{
    public class UrlConfiguration : IEntityTypeConfiguration<Url>
    {
        public void Configure(EntityTypeBuilder<Url> builder)
        {
            builder.ToTable(nameof(Url)).HasKey(u => u.Id);

            builder.Property(u => u.ValidFromTime).HasColumnType("datetime2");

            builder.Property(u => u.ValidUntilTime).HasColumnType("datetime2");

            builder.Property(u => u.IntervieweeName).HasMaxLength(128);

            builder.HasOne(u => u.Test).WithMany(t => t.Urls).HasForeignKey(u => u.TestId).OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(u => u.TestResults).WithOne(tr => tr.Url).HasForeignKey(tr => tr.UrlId);
        }
    }
}
