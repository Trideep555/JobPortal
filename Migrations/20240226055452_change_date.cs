using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DREAMCatcher.Migrations
{
    /// <inheritdoc />
    public partial class change_date : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "end_date",
                table: "Experiences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "start_date",
                table: "Experiences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "end_date",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "start_date",
                table: "Experiences");
        }
    }
}
