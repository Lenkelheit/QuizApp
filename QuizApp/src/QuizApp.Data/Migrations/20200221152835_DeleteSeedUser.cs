﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizApp.Data.Migrations
{
    public partial class DeleteSeedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Password", "Username" },
                values: new object[] { 1, "john@gmail.com", "1111", "John" });
        }
    }
}
