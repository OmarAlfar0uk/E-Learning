using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_AppUsers_AppUserId1",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_AppUsers_AppUserId1",
                table: "Progress");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_AppUsers_CreatedById1",
                table: "Quizzes");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AppUsers_AppUserId1",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_AppUserId1",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Progress_AppUserId1",
                table: "Progress");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_AppUserId1",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Progress");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Enrollments");

            migrationBuilder.RenameColumn(
                name: "AppUserId1",
                table: "Reviews",
                newName: "ReportReason");

            migrationBuilder.RenameColumn(
                name: "CreatedById1",
                table: "Quizzes",
                newName: "AppUsersId");

            migrationBuilder.RenameIndex(
                name: "IX_Quizzes_CreatedById1",
                table: "Quizzes",
                newName: "IX_Quizzes_AppUsersId");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Reviews",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<bool>(
                name: "IsReported",
                table: "Reviews",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Quizzes",
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
                table: "Enrollments",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedAt",
                table: "Enrollments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ProgressPercentage",
                table: "Enrollments",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "PromoVideoUrl",
                table: "Courses",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Courses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Courses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Courses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AppUserId",
                table: "Reviews",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_CreatedById",
                table: "Quizzes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Progress_AppUserId",
                table: "Progress",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_AppUserId",
                table: "Enrollments",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_AppUsers_AppUserId",
                table: "Enrollments",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_AppUsers_AppUserId",
                table: "Progress",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_AppUsers_AppUsersId",
                table: "Quizzes",
                column: "AppUsersId",
                principalTable: "AppUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_AppUsers_CreatedById",
                table: "Quizzes",
                column: "CreatedById",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AppUsers_AppUserId",
                table: "Reviews",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_AppUsers_AppUserId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_AppUsers_AppUserId",
                table: "Progress");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_AppUsers_AppUsersId",
                table: "Quizzes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_AppUsers_CreatedById",
                table: "Quizzes");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AppUsers_AppUserId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_AppUserId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_CreatedById",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Progress_AppUserId",
                table: "Progress");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_AppUserId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "IsReported",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "ProgressPercentage",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "ReportReason",
                table: "Reviews",
                newName: "AppUserId1");

            migrationBuilder.RenameColumn(
                name: "AppUsersId",
                table: "Quizzes",
                newName: "CreatedById1");

            migrationBuilder.RenameIndex(
                name: "IX_Quizzes_AppUsersId",
                table: "Quizzes",
                newName: "IX_Quizzes_CreatedById1");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Reviews",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedById",
                table: "Quizzes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

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
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Enrollments",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Enrollments",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PromoVideoUrl",
                table: "Courses",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AppUserId1",
                table: "Reviews",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Progress_AppUserId1",
                table: "Progress",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_AppUserId1",
                table: "Enrollments",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_AppUsers_AppUserId1",
                table: "Enrollments",
                column: "AppUserId1",
                principalTable: "AppUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_AppUsers_AppUserId1",
                table: "Progress",
                column: "AppUserId1",
                principalTable: "AppUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_AppUsers_CreatedById1",
                table: "Quizzes",
                column: "CreatedById1",
                principalTable: "AppUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AppUsers_AppUserId1",
                table: "Reviews",
                column: "AppUserId1",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }
    }
}
