using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizApp.Entities;

namespace QuizApp.Data.Configuration
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.ToTable(nameof(Test)).HasKey(t => t.Id);

            builder.Property(t => t.Title).IsRequired().HasMaxLength(128);

            builder.Property(t => t.Description).HasMaxLength(512);

            builder.Property(t => t.TimeLimitSeconds).HasColumnType("time");

            builder.Property(t => t.LastModifiedDate).HasColumnType("datetime2");

            builder.HasOne(t => t.Author).WithMany(u => u.CreatedTests).HasForeignKey(t => t.AuthorId);

            builder.HasMany(t => t.Urls).WithOne(u => u.Test).HasForeignKey(u => u.TestId).OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(t => t.TestQuestions).WithOne(q => q.Test).HasForeignKey(q => q.TestId);
        }
    }
}
