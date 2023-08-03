using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TA_PROJECT_CyclecountAPI.Migrations
{
    /// <inheritdoc />
    public partial class Lx17Abcin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Abcin",
                table: "Lx17Log",
                type: "nvarchar(16)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Abcin",
                table: "Lx17",
                type: "nvarchar(16)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abcin",
                table: "Lx17Log");

            migrationBuilder.DropColumn(
                name: "Abcin",
                table: "Lx17");
        }
    }
}
