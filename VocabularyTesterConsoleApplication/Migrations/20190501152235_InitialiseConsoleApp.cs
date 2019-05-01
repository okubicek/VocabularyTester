using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VocabularyPracticeConsoleApplication.Migrations
{
    public partial class InitialiseConsoleApp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AskedQuestion = table.Column<string>(nullable: true),
                    CorrectAnswer = table.Column<string>(nullable: true),
                    Asnwered = table.Column<string>(nullable: true),
                    IsCorrect = table.Column<bool>(nullable: false),
                    AnsweredAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");
        }
    }
}
