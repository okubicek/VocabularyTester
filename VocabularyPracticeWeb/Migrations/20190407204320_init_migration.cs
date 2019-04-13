using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace VocabularyPracticeWeb.Migrations
{
    public partial class init_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			string sqlScript = @"Migrations\20190407204320_init_migration.sql";
			migrationBuilder.Sql(File.ReadAllText(sqlScript));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
