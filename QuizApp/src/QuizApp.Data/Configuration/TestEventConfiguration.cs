using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizApp.Entities;

namespace QuizApp.Data.Configuration
{
    public class TestEventConfiguration : IEntityTypeConfiguration<TestEvent>
    {
        public void Configure(EntityTypeBuilder<TestEvent> builder)
        {
            builder.ToTable(nameof(TestEvent)).HasKey(e => e.Id);

            builder.Property(tr => tr.StartTime).HasColumnType("datetime2");
        }
    }
}
