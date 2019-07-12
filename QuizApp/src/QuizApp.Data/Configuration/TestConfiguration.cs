﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizApp.Entities;

namespace QuizApp.Data.Configuration
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.Property(t => t.Title).IsRequired();

            builder.Property(t => t.TimeLimitSeconds).IsRequired().HasColumnType("time");

            builder.Property(t => t.LastModifiedDate).IsRequired().HasColumnType("datetime2");

            builder.HasOne(t => t.Author).WithMany(u => u.Tests);

            builder.HasMany(t => t.Urls).WithOne(u => u.Test);

            builder.HasMany(t => t.TestQuestions).WithOne(q => q.Test);
        }
    }
}
