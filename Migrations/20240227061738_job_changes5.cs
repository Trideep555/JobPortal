using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DREAMCatcher.Migrations
{
    /// <inheritdoc />
    public partial class job_changes5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "Job",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Job");
        }
    }
}
