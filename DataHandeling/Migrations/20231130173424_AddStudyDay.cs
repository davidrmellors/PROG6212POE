using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataHandeling.Migrations
{
    public partial class AddStudyDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudyDay",
                table: "Modules",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudyDay",
                table: "Modules");
        }
    }
}
