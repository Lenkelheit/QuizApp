using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizApp.Data.Migrations
{
    public partial class DoubleScoreInTestResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Score",
                table: "TestResult",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "TestResult",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
