using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimViec.Migrations
{
    /// <inheritdoc />
    public partial class v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "StatusJobs",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "StatusJobs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatusJobs_UserId",
                table: "StatusJobs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StatusJobs_AspNetUsers_UserId",
                table: "StatusJobs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatusJobs_AspNetUsers_UserId",
                table: "StatusJobs");

            migrationBuilder.DropIndex(
                name: "IX_StatusJobs_UserId",
                table: "StatusJobs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StatusJobs");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "StatusJobs",
                newName: "ID");
        }
    }
}
