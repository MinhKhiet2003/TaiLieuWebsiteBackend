using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaiLieuWebsiteBackend.Migrations
{
    /// <inheritdoc />
    public partial class addContentTypeStar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Stars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Stars");
        }
    }
}
