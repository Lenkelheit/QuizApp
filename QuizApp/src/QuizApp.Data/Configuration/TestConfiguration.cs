using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizApp.Entities;
using QuizApp.Data.EntitiesConstraints;

namespace QuizApp.Data.Configuration
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.ToTable(nameof(Test));

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title).IsRequired().HasMaxLength(TestConstraints.TitleMaxLength);

            builder.Property(t => t.Description).HasMaxLength(TestConstraints.DescriptionMaxLength);

            builder.Property(t => t.TimeLimitSeconds).HasColumnType("time");

            builder.Property(t => t.LastModifiedDate).HasColumnType("datetime2");

            builder.HasOne(t => t.Author).WithMany(u => u.CreatedTests).HasForeignKey(t => t.AuthorId);

            builder.HasMany(t => t.Urls).WithOne(u => u.Test).HasForeignKey(u => u.TestId);

            builder.HasMany(t => t.TestQuestions).WithOne(q => q.Test).HasForeignKey(q => q.TestId);
        }
    }
}
