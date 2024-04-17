using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimViec.Migrations
{
    /// <inheritdoc />
    public partial class AddJobnametoStatusJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Jobname",
                table: "StatusJobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Jobname",
                table: "StatusJobs");
        }
    }
}
