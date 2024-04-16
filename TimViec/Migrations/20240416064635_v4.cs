using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimViec.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applications_Jobs_JobId",
                table: "applications");

            migrationBuilder.DropColumn(
                name: "Id_rank",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Id_skill",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Id_type_work",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Id_city",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Id_job",
                table: "applications");

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "applications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_applications_Jobs_JobId",
                table: "applications",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applications_Jobs_JobId",
                table: "applications");

            migrationBuilder.AddColumn<int>(
                name: "Id_rank",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id_skill",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id_type_work",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id_city",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "applications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id_job",
                table: "applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_applications_Jobs_JobId",
                table: "applications",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }
    }
}
