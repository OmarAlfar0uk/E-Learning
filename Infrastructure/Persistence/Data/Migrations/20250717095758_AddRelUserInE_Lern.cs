using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRelUserInE_Lern : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Reviews",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Reviews",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Quizzes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById1",
                table: "Quizzes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Progress",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Progress",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Enrollments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddColumn<string>(
                name: "InstructorId",
                table: "Courses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AppUserId1",
                table: "Reviews",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_CreatedById1",
                table: "Quizzes",
                column: "CreatedById1");

            migrationBuilder.CreateIndex(
                name: "IX_Progress_AppUserId1",
                table: "Progress",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_AppUserId1",
                table: "Enrollments",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_InstructorId",
                table: "Courses",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AppUsers_InstructorId",
                table: "Courses",
                column: "InstructorId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AppUsers_InstructorId",
                table: "Courses");

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

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_AppUserId1",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_CreatedById1",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Progress_AppUserId1",
                table: "Progress");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_AppUserId1",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Courses_InstructorId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "CreatedById1",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Progress");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Progress");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "Courses");

            migrationBuilder.AlterColumn<string>(
                name: "PromoVideoUrl",
                table: "Courses",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);
        }
    }
}
