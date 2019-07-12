﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using QuizApp.Entities;

namespace QuizApp.Data.Configuration
{
    public class TestResultConfiguration : IEntityTypeConfiguration<TestResult>
    {
        public void Configure(EntityTypeBuilder<TestResult> builder)
        {
            builder.Property(tr => tr.IntervieweeName).IsRequired();

            builder.Property(tr => tr.PassingStartTime).IsRequired().HasColumnType("datetime2");

            builder.Property(tr => tr.PassingEndTime).IsRequired().HasColumnType("datetime2");

            builder.HasOne(tr => tr.Url).WithMany(u => u.TestResults);

            builder.HasMany(tr => tr.ResultAnswers).WithOne(tr => tr.Result);
        }
    }
}
