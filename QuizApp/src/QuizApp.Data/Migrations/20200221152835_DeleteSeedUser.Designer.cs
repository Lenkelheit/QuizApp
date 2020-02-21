﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuizApp.Data.Context;

namespace QuizApp.Data.Migrations
{
    [DbContext(typeof(QuizAppDbContext))]
    [Migration("20200221152835_DeleteSeedUser")]
    partial class DeleteSeedUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("QuizApp.Entities.ResultAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("QuestionId");

                    b.Property<int>("ResultId");

                    b.Property<TimeSpan>("TimeTakenSeconds")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("ResultId");

                    b.ToTable("ResultAnswer");
                });

            modelBuilder.Entity("QuizApp.Entities.ResultAnswerOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("OptionId");

                    b.Property<int>("ResultAnswerId");

                    b.HasKey("Id");

                    b.HasIndex("OptionId");

                    b.HasIndex("ResultAnswerId");

                    b.ToTable("ResultAnswerOption");
                });

            modelBuilder.Entity("QuizApp.Entities.Test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthorId");

                    b.Property<string>("Description")
                        .HasMaxLength(512);

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("TimeLimitSeconds")
                        .HasColumnType("time");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Test");
                });

            modelBuilder.Entity("QuizApp.Entities.TestEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EventType");

                    b.Property<string>("Payload");

                    b.Property<Guid>("SessionId");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("TestEvent");
                });

            modelBuilder.Entity("QuizApp.Entities.TestQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Hint")
                        .HasMaxLength(256);

                    b.Property<int>("TestId");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(512);

                    b.Property<TimeSpan>("TimeLimitSeconds")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("TestQuestion");
                });

            modelBuilder.Entity("QuizApp.Entities.TestQuestionOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsRight");

                    b.Property<int>("QuestionId");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("TestQuestionOption");
                });

            modelBuilder.Entity("QuizApp.Entities.TestResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IntervieweeName")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<DateTime>("PassingEndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PassingStartTime")
                        .HasColumnType("datetime2");

                    b.Property<double>("Score");

                    b.Property<int>("UrlId");

                    b.HasKey("Id");

                    b.HasIndex("UrlId");

                    b.ToTable("TestResult");
                });

            modelBuilder.Entity("QuizApp.Entities.Url", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IntervieweeName")
                        .HasMaxLength(128);

                    b.Property<int?>("NumberOfRuns");

                    b.Property<int?>("TestId");

                    b.Property<DateTime>("ValidFromTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ValidUntilTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("Url");
                });

            modelBuilder.Entity("QuizApp.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("QuizApp.Entities.ResultAnswer", b =>
                {
                    b.HasOne("QuizApp.Entities.TestQuestion", "Question")
                        .WithMany("ResultAnswers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("QuizApp.Entities.TestResult", "Result")
                        .WithMany("ResultAnswers")
                        .HasForeignKey("ResultId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QuizApp.Entities.ResultAnswerOption", b =>
                {
                    b.HasOne("QuizApp.Entities.TestQuestionOption", "Option")
                        .WithMany("ResultAnswerOptions")
                        .HasForeignKey("OptionId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("QuizApp.Entities.ResultAnswer", "ResultAnswer")
                        .WithMany("ResultAnswerOptions")
                        .HasForeignKey("ResultAnswerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QuizApp.Entities.Test", b =>
                {
                    b.HasOne("QuizApp.Entities.User", "Author")
                        .WithMany("CreatedTests")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QuizApp.Entities.TestQuestion", b =>
                {
                    b.HasOne("QuizApp.Entities.Test", "Test")
                        .WithMany("TestQuestions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QuizApp.Entities.TestQuestionOption", b =>
                {
                    b.HasOne("QuizApp.Entities.TestQuestion", "Question")
                        .WithMany("TestQuestionOptions")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QuizApp.Entities.TestResult", b =>
                {
                    b.HasOne("QuizApp.Entities.Url", "Url")
                        .WithMany("TestResults")
                        .HasForeignKey("UrlId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QuizApp.Entities.Url", b =>
                {
                    b.HasOne("QuizApp.Entities.Test", "Test")
                        .WithMany("Urls")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}
