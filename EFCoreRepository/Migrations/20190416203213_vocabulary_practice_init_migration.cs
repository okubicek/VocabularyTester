using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace VocabularyPracticeEFCoreRepository.Migrations
{
    public partial class vocabulary_practice_init_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			string sqlScript = @"..\EFCoreRepository\Migrations\20190416203213_vocabulary_practice_init_migration.sql";
			migrationBuilder.Sql(File.ReadAllText(sqlScript));
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessonVocabulary");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}
