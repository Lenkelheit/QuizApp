using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizApp.Entities;
using QuizApp.Data.EntitiesConstraints;

namespace QuizApp.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username).IsRequired().HasMaxLength(UserConstraints.UsernameMaxLength);

            builder.Property(u => u.Email).IsRequired().HasMaxLength(UserConstraints.EmailMaxLength);

            builder.Property(u => u.Password).IsRequired().HasMaxLength(UserConstraints.PasswordMaxLength);

            builder.HasMany(u => u.CreatedTests).WithOne(t => t.Author).HasForeignKey(t => t.AuthorId);
        }
    }
}
