using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class FourthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstitutionId",
                table: "Chats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_InstitutionId",
                table: "Chats",
                column: "InstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Institutions_InstitutionId",
                table: "Chats",
                column: "InstitutionId",
                principalTable: "Institutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Institutions_InstitutionId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_InstitutionId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "InstitutionId",
                table: "Chats");
        }
    }
}
