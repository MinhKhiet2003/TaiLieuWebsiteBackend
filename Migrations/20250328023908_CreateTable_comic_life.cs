using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaiLieuWebsiteBackend.Migrations
{
    /// <inheritdoc />
    public partial class CreateTable_comic_life : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "uploaded_by",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Comic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comic_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Uploaded_by = table.Column<int>(type: "int", nullable: false),
                    Category_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comic_Categories_Category_id",
                        column: x => x.Category_id,
                        principalTable: "Categories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comic_Users_Uploaded_by",
                        column: x => x.Uploaded_by,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Life",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Uploaded_by = table.Column<int>(type: "int", nullable: false),
                    Category_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Life", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Life_Categories_Category_id",
                        column: x => x.Category_id,
                        principalTable: "Categories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Life_Users_Uploaded_by",
                        column: x => x.Uploaded_by,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Videos_uploaded_by",
                table: "Videos",
                column: "uploaded_by");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_uploaded_by",
                table: "Categories",
                column: "uploaded_by");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_user_id",
                table: "Categories",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comic_Category_id",
                table: "Comic",
                column: "Category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comic_Uploaded_by",
                table: "Comic",
                column: "Uploaded_by");

            migrationBuilder.CreateIndex(
                name: "IX_Life_Category_id",
                table: "Life",
                column: "Category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Life_Uploaded_by",
                table: "Life",
                column: "Uploaded_by");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_uploaded_by",
                table: "Categories",
                column: "uploaded_by",
                principalTable: "Users",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_user_id",
                table: "Categories",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Users_uploaded_by",
                table: "Videos",
                column: "uploaded_by",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_uploaded_by",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_user_id",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Users_uploaded_by",
                table: "Videos");

            migrationBuilder.DropTable(
                name: "Comic");

            migrationBuilder.DropTable(
                name: "Life");

            migrationBuilder.DropIndex(
                name: "IX_Videos_uploaded_by",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Categories_uploaded_by",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_user_id",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "uploaded_by",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "Categories");
        }
    }
}
