using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizApp.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(maxLength: 128, nullable: false),
                    Email = table.Column<string>(maxLength: 128, nullable: false),
                    Password = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    Description = table.Column<string>(maxLength: 512, nullable: true),
                    TimeLimitSeconds = table.Column<TimeSpan>(type: "time", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Test_User_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(maxLength: 512, nullable: false),
                    Hint = table.Column<string>(maxLength: 256, nullable: true),
                    TimeLimitSeconds = table.Column<TimeSpan>(type: "time", nullable: false),
                    TestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestQuestion_Test_TestId",
                        column: x => x.TestId,
                        principalTable: "Test",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Url",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NumberOfRuns = table.Column<int>(nullable: true),
                    ValidFromTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidUntilTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IntervieweeName = table.Column<string>(maxLength: 128, nullable: true),
                    TestId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Url", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Url_Test_TestId",
                        column: x => x.TestId,
                        principalTable: "Test",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TestQuestionOption",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(maxLength: 256, nullable: false),
                    IsRight = table.Column<bool>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestQuestionOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestQuestionOption_TestQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "TestQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IntervieweeName = table.Column<string>(maxLength: 128, nullable: false),
                    PassingStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PassingEndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Score = table.Column<int>(nullable: false),
                    UrlId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestResult_Url_UrlId",
                        column: x => x.UrlId,
                        principalTable: "Url",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResultAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimeTakenSeconds = table.Column<TimeSpan>(type: "time", nullable: false),
                    QuestionId = table.Column<int>(nullable: true),
                    ResultId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResultAnswer_TestQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "TestQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ResultAnswer_TestResult_ResultId",
                        column: x => x.ResultId,
                        principalTable: "TestResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResultAnswerOption",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OptionId = table.Column<int>(nullable: true),
                    ResultAnswerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultAnswerOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResultAnswerOption_TestQuestionOption_OptionId",
                        column: x => x.OptionId,
                        principalTable: "TestQuestionOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ResultAnswerOption_ResultAnswer_ResultAnswerId",
                        column: x => x.ResultAnswerId,
                        principalTable: "ResultAnswer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResultAnswer_QuestionId",
                table: "ResultAnswer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultAnswer_ResultId",
                table: "ResultAnswer",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultAnswerOption_OptionId",
                table: "ResultAnswerOption",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultAnswerOption_ResultAnswerId",
                table: "ResultAnswerOption",
                column: "ResultAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Test_AuthorId",
                table: "Test",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestion_TestId",
                table: "TestQuestion",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestionOption_QuestionId",
                table: "TestQuestionOption",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResult_UrlId",
                table: "TestResult",
                column: "UrlId");

            migrationBuilder.CreateIndex(
                name: "IX_Url_TestId",
                table: "Url",
                column: "TestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResultAnswerOption");

            migrationBuilder.DropTable(
                name: "TestQuestionOption");

            migrationBuilder.DropTable(
                name: "ResultAnswer");

            migrationBuilder.DropTable(
                name: "TestQuestion");

            migrationBuilder.DropTable(
                name: "TestResult");

            migrationBuilder.DropTable(
                name: "Url");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
