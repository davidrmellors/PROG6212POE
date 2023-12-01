using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataHandeling.Migrations
{
    public partial class AddUserToModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Modules",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
