using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizApp.Entities;

namespace QuizApp.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User)).HasKey(u => u.Id);

            builder.Property(u => u.Username).IsRequired().HasMaxLength(128);

            builder.Property(u => u.Email).IsRequired().HasMaxLength(128);

            builder.Property(u => u.Password).IsRequired().HasMaxLength(256);

            builder.HasMany(u => u.CreatedTests).WithOne(t => t.Author).HasForeignKey(t => t.AuthorId);
        }
    }
}
