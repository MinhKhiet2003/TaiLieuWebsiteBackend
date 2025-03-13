using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaiLieuWebsiteBackend.Migrations
{
    /// <inheritdoc />
    public partial class remove_topic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Users_uploaded_by",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_uploaded_by",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "topic",
                table: "Exercises");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "topic",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_uploaded_by",
                table: "Videos",
                column: "uploaded_by");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Users_uploaded_by",
                table: "Videos",
                column: "uploaded_by",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
