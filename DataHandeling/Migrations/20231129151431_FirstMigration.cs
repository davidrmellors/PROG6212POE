using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataHandeling.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    ModuleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ModuleCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModuleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfCredits = table.Column<int>(type: "int", nullable: false),
                    ClassHoursPerWeek = table.Column<double>(type: "float", nullable: false),
                    NumberOfWeeks = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.ModuleID);
                    table.ForeignKey(
                        name: "FK_Modules_Users_Username1",
                        column: x => x.Username1,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudyHours",
                columns: table => new
                {
                    StudyHoursID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeekNumber = table.Column<int>(type: "int", nullable: false),
                    RemainingHours = table.Column<double>(type: "float", nullable: false),
                    ModuleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyHours", x => x.StudyHoursID);
                    table.ForeignKey(
                        name: "FK_StudyHours_Modules_ModuleID",
                        column: x => x.ModuleID,
                        principalTable: "Modules",
                        principalColumn: "ModuleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Modules_Username1",
                table: "Modules",
                column: "Username1");

            migrationBuilder.CreateIndex(
                name: "IX_StudyHours_ModuleID",
                table: "StudyHours",
                column: "ModuleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudyHours");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
