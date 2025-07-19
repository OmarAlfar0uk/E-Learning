using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Identity.Migrations
{
    /// <inheritdoc />
    public partial class FixAppUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Users_AppUserId1",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Users_AppUserId1",
                table: "Progress");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Users_AppUserId1",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_AppUserId1",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Progress_AppUserId1",
                table: "Progress");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_AppUserId1",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Progress");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Enrollment");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Review",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Progress",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Enrollment",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Review_AppUserId",
                table: "Review",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Progress_AppUserId",
                table: "Progress",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_AppUserId",
                table: "Enrollment",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Users_AppUserId",
                table: "Enrollment",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Users_AppUserId",
                table: "Progress",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Users_AppUserId",
                table: "Review",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Users_AppUserId",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Users_AppUserId",
                table: "Progress");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Users_AppUserId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_AppUserId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Progress_AppUserId",
                table: "Progress");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_AppUserId",
                table: "Enrollment");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Review",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Review",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Progress",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Progress",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Enrollment",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Enrollment",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Review_AppUserId1",
                table: "Review",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Progress_AppUserId1",
                table: "Progress",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_AppUserId1",
                table: "Enrollment",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Users_AppUserId1",
                table: "Enrollment",
                column: "AppUserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Users_AppUserId1",
                table: "Progress",
                column: "AppUserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Users_AppUserId1",
                table: "Review",
                column: "AppUserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
