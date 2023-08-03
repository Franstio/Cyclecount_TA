using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TA_PROJECT_CyclecountAPI.Migrations
{
    /// <inheritdoc />
    public partial class Updt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Action",
                table: "Lx17Log",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Action",
                table: "Lx17Log");
        }
    }
}
