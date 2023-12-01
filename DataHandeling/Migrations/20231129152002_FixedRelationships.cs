using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataHandeling.Migrations
{
    public partial class FixedRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Users_Username1",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Modules_Username1",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "Username1",
                table: "Modules");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Modules",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_Username",
                table: "Modules",
                column: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Users_Username",
                table: "Modules",
                column: "Username",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Users_Username",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Modules_Username",
                table: "Modules");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Modules",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Username1",
                table: "Modules",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_Username1",
                table: "Modules",
                column: "Username1");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Users_Username1",
                table: "Modules",
                column: "Username1",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
