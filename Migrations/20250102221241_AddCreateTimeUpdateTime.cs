using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaiLieuWebsiteBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddCreateTimeUpdateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "upload_date",
                table: "Videos",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "upload_date",
                table: "Games",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "upload_date",
                table: "Exercises",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "upload_date",
                table: "Documents",
                newName: "updated_at");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "Videos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "Games",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "Exercises",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "Documents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "Classes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "Classes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Videos",
                newName: "upload_date");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Games",
                newName: "upload_date");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Exercises",
                newName: "upload_date");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Documents",
                newName: "upload_date");
        }
    }
}
