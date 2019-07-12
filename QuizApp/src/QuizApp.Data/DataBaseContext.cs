﻿using Microsoft.EntityFrameworkCore;
using QuizApp.Entities;

namespace QuizApp.Data
{
    public class DataBaseContext : DbContext
    {
        // CONSTRUCTORS
        public DataBaseContext()
        {
            Database.EnsureCreated();
        }

        // PROPERTIES
        public DbSet<User> Users { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Url> Urls { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<ResultAnswer> ResultAnswers { get; set; }
        public DbSet<ResultAnswerOption> ResultAnswerOptions { get; set; }
        public DbSet<TestQuestion> TestQuestions { get; set; }
        public DbSet<TestQuestionOption> TestQuestionOptions { get; set; }

        // METHODS
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=QuizAppDB;Trusted_Connection=True;");
        }
    }
}
