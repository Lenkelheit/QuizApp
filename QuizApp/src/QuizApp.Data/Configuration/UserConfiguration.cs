using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizApp.Entities;

namespace QuizApp.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Username).IsRequired().HasMaxLength(EntitiesConstraints.USERNAME_MAX_LENGTH);

            builder.Property(u => u.Email).IsRequired().HasMaxLength(EntitiesConstraints.EMAIL_MAX_LENGTH);

            builder.Property(u => u.Password).IsRequired().HasMaxLength(EntitiesConstraints.PASSWORD_MAX_LENGTH);

            builder.HasMany(u => u.CreatedTests).WithOne(t => t.Author).HasForeignKey(t => t.AuthorId);
        }
    }
}
