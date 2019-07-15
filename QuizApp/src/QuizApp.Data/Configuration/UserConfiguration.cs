using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizApp.Entities;

namespace QuizApp.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Username).IsRequired();

            builder.Property(u => u.Email).IsRequired();

            builder.Property(u => u.Password).IsRequired();

            builder.HasMany(u => u.CreatedTests).WithOne(t => t.Author).HasForeignKey(t => t.AuthorId);
        }
    }
}
