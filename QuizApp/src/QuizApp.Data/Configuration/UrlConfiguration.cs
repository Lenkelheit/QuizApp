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

            builder.HasOne(u => u.Test).WithMany(t => t.Urls);

            builder.HasMany(u => u.TestResults).WithOne(tr => tr.Url);
        }
    }
}
