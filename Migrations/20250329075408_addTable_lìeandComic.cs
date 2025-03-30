using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaiLieuWebsiteBackend.Migrations
{
    /// <inheritdoc />
    public partial class addTable_lìeandComic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comic_Categories_Category_id",
                table: "Comic");

            migrationBuilder.DropForeignKey(
                name: "FK_Comic_Users_Uploaded_by",
                table: "Comic");

            migrationBuilder.DropForeignKey(
                name: "FK_Life_Categories_Category_id",
                table: "Life");

            migrationBuilder.DropForeignKey(
                name: "FK_Life_Users_Uploaded_by",
                table: "Life");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Life",
                table: "Life");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comic",
                table: "Comic");

            migrationBuilder.RenameTable(
                name: "Life",
                newName: "Lifes");

            migrationBuilder.RenameTable(
                name: "Comic",
                newName: "Comics");

            migrationBuilder.RenameIndex(
                name: "IX_Life_Uploaded_by",
                table: "Lifes",
                newName: "IX_Lifes_Uploaded_by");

            migrationBuilder.RenameIndex(
                name: "IX_Life_Category_id",
                table: "Lifes",
                newName: "IX_Lifes_Category_id");

            migrationBuilder.RenameIndex(
                name: "IX_Comic_Uploaded_by",
                table: "Comics",
                newName: "IX_Comics_Uploaded_by");

            migrationBuilder.RenameIndex(
                name: "IX_Comic_Category_id",
                table: "Comics",
                newName: "IX_Comics_Category_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lifes",
                table: "Lifes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comics",
                table: "Comics",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comics_Categories_Category_id",
                table: "Comics",
                column: "Category_id",
                principalTable: "Categories",
                principalColumn: "category_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comics_Users_Uploaded_by",
                table: "Comics",
                column: "Uploaded_by",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lifes_Categories_Category_id",
                table: "Lifes",
                column: "Category_id",
                principalTable: "Categories",
                principalColumn: "category_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lifes_Users_Uploaded_by",
                table: "Lifes",
                column: "Uploaded_by",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comics_Categories_Category_id",
                table: "Comics");

            migrationBuilder.DropForeignKey(
                name: "FK_Comics_Users_Uploaded_by",
                table: "Comics");

            migrationBuilder.DropForeignKey(
                name: "FK_Lifes_Categories_Category_id",
                table: "Lifes");

            migrationBuilder.DropForeignKey(
                name: "FK_Lifes_Users_Uploaded_by",
                table: "Lifes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lifes",
                table: "Lifes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comics",
                table: "Comics");

            migrationBuilder.RenameTable(
                name: "Lifes",
                newName: "Life");

            migrationBuilder.RenameTable(
                name: "Comics",
                newName: "Comic");

            migrationBuilder.RenameIndex(
                name: "IX_Lifes_Uploaded_by",
                table: "Life",
                newName: "IX_Life_Uploaded_by");

            migrationBuilder.RenameIndex(
                name: "IX_Lifes_Category_id",
                table: "Life",
                newName: "IX_Life_Category_id");

            migrationBuilder.RenameIndex(
                name: "IX_Comics_Uploaded_by",
                table: "Comic",
                newName: "IX_Comic_Uploaded_by");

            migrationBuilder.RenameIndex(
                name: "IX_Comics_Category_id",
                table: "Comic",
                newName: "IX_Comic_Category_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Life",
                table: "Life",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comic",
                table: "Comic",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comic_Categories_Category_id",
                table: "Comic",
                column: "Category_id",
                principalTable: "Categories",
                principalColumn: "category_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comic_Users_Uploaded_by",
                table: "Comic",
                column: "Uploaded_by",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Life_Categories_Category_id",
                table: "Life",
                column: "Category_id",
                principalTable: "Categories",
                principalColumn: "category_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Life_Users_Uploaded_by",
                table: "Life",
                column: "Uploaded_by",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
