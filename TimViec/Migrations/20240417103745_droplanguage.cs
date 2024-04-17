using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimViec.Migrations
{
    /// <inheritdoc />
    public partial class droplanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "R1_Language",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "R2_Language",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "R3_Language",
                table: "Companies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "R1_Language",
                table: "Companies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "R2_Language",
                table: "Companies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "R3_Language",
                table: "Companies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
