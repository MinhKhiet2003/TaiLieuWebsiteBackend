using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaiLieuWebsiteBackend.Migrations
{
    /// <inheritdoc />
    public partial class updateForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    class_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.class_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password_hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ProfilePicturePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    class_id = table.Column<int>(type: "int", nullable: false),
                    uploaded_by = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.category_id);
                    table.ForeignKey(
                        name: "FK_Categories_Classes_class_id",
                        column: x => x.class_id,
                        principalTable: "Classes",
                        principalColumn: "class_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Categories_Users_uploaded_by",
                        column: x => x.uploaded_by,
                        principalTable: "Users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_Categories_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Comics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comic_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Uploaded_by = table.Column<int>(type: "int", nullable: false),
                    Category_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comics_Categories_Category_id",
                        column: x => x.Category_id,
                        principalTable: "Categories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comics_Users_Uploaded_by",
                        column: x => x.Uploaded_by,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    document_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    file_path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    category_id = table.Column<int>(type: "int", nullable: false),
                    uploaded_by = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.document_id);
                    table.ForeignKey(
                        name: "FK_Documents_Categories_category_id",
                        column: x => x.category_id,
                        principalTable: "Categories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_Users_uploaded_by",
                        column: x => x.uploaded_by,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    exercise_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    difficulty = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    category_id = table.Column<int>(type: "int", nullable: false),
                    uploaded_by = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.exercise_id);
                    table.ForeignKey(
                        name: "FK_Exercises_Categories_category_id",
                        column: x => x.category_id,
                        principalTable: "Categories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exercises_Users_uploaded_by",
                        column: x => x.uploaded_by,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    game_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    game_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    classify = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    category_id = table.Column<int>(type: "int", nullable: false),
                    uploaded_by = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.game_id);
                    table.ForeignKey(
                        name: "FK_Games_Categories_category_id",
                        column: x => x.category_id,
                        principalTable: "Categories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Users_uploaded_by",
                        column: x => x.uploaded_by,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lifes",
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
                    table.PrimaryKey("PK_Lifes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lifes_Categories_Category_id",
                        column: x => x.Category_id,
                        principalTable: "Categories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lifes_Users_Uploaded_by",
                        column: x => x.Uploaded_by,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    video_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    video_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    category_id = table.Column<int>(type: "int", nullable: false),
                    uploaded_by = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.video_id);
                    table.ForeignKey(
                        name: "FK_Videos_Categories_category_id",
                        column: x => x.category_id,
                        principalTable: "Categories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Videos_Users_uploaded_by",
                        column: x => x.uploaded_by,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    comment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    document_id = table.Column<int>(type: "int", nullable: true),
                    game_id = table.Column<int>(type: "int", nullable: true),
                    video_id = table.Column<int>(type: "int", nullable: true),
                    comic_id = table.Column<int>(type: "int", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.comment_id);
                    table.ForeignKey(
                        name: "FK_Comments_Comics_comic_id",
                        column: x => x.comic_id,
                        principalTable: "Comics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Documents_document_id",
                        column: x => x.document_id,
                        principalTable: "Documents",
                        principalColumn: "document_id");
                    table.ForeignKey(
                        name: "FK_Comments_Games_game_id",
                        column: x => x.game_id,
                        principalTable: "Games",
                        principalColumn: "game_id");
                    table.ForeignKey(
                        name: "FK_Comments_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_Comments_Videos_video_id",
                        column: x => x.video_id,
                        principalTable: "Videos",
                        principalColumn: "video_id");
                });

            migrationBuilder.CreateTable(
                name: "Stars",
                columns: table => new
                {
                    star_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    total_star = table.Column<int>(type: "int", nullable: false),
                    document_id = table.Column<int>(type: "int", nullable: true),
                    exercise_id = table.Column<int>(type: "int", nullable: true),
                    game_id = table.Column<int>(type: "int", nullable: true),
                    video_id = table.Column<int>(type: "int", nullable: true),
                    comic_id = table.Column<int>(type: "int", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stars", x => x.star_id);
                    table.ForeignKey(
                        name: "FK_Stars_Comics_comic_id",
                        column: x => x.comic_id,
                        principalTable: "Comics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Stars_Documents_document_id",
                        column: x => x.document_id,
                        principalTable: "Documents",
                        principalColumn: "document_id");
                    table.ForeignKey(
                        name: "FK_Stars_Exercises_exercise_id",
                        column: x => x.exercise_id,
                        principalTable: "Exercises",
                        principalColumn: "exercise_id");
                    table.ForeignKey(
                        name: "FK_Stars_Games_game_id",
                        column: x => x.game_id,
                        principalTable: "Games",
                        principalColumn: "game_id");
                    table.ForeignKey(
                        name: "FK_Stars_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_Stars_Videos_video_id",
                        column: x => x.video_id,
                        principalTable: "Videos",
                        principalColumn: "video_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_class_id",
                table: "Categories",
                column: "class_id");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_uploaded_by",
                table: "Categories",
                column: "uploaded_by");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_user_id",
                table: "Categories",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comics_Category_id",
                table: "Comics",
                column: "Category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comics_Uploaded_by",
                table: "Comics",
                column: "Uploaded_by");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_comic_id",
                table: "Comments",
                column: "comic_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_document_id",
                table: "Comments",
                column: "document_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_game_id",
                table: "Comments",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_user_id",
                table: "Comments",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_video_id",
                table: "Comments",
                column: "video_id");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_category_id",
                table: "Documents",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_uploaded_by",
                table: "Documents",
                column: "uploaded_by");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_category_id",
                table: "Exercises",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_uploaded_by",
                table: "Exercises",
                column: "uploaded_by");

            migrationBuilder.CreateIndex(
                name: "IX_Games_category_id",
                table: "Games",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Games_uploaded_by",
                table: "Games",
                column: "uploaded_by");

            migrationBuilder.CreateIndex(
                name: "IX_Lifes_Category_id",
                table: "Lifes",
                column: "Category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Lifes_Uploaded_by",
                table: "Lifes",
                column: "Uploaded_by");

            migrationBuilder.CreateIndex(
                name: "IX_Stars_comic_id",
                table: "Stars",
                column: "comic_id");

            migrationBuilder.CreateIndex(
                name: "IX_Stars_document_id",
                table: "Stars",
                column: "document_id");

            migrationBuilder.CreateIndex(
                name: "IX_Stars_exercise_id",
                table: "Stars",
                column: "exercise_id");

            migrationBuilder.CreateIndex(
                name: "IX_Stars_game_id",
                table: "Stars",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "IX_Stars_user_id_comic_id",
                table: "Stars",
                columns: new[] { "user_id", "comic_id" },
                unique: true,
                filter: "[comic_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Stars_user_id_document_id",
                table: "Stars",
                columns: new[] { "user_id", "document_id" },
                unique: true,
                filter: "[document_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Stars_user_id_exercise_id",
                table: "Stars",
                columns: new[] { "user_id", "exercise_id" },
                unique: true,
                filter: "[exercise_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Stars_user_id_game_id",
                table: "Stars",
                columns: new[] { "user_id", "game_id" },
                unique: true,
                filter: "[game_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Stars_user_id_video_id",
                table: "Stars",
                columns: new[] { "user_id", "video_id" },
                unique: true,
                filter: "[video_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Stars_video_id",
                table: "Stars",
                column: "video_id");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_category_id",
                table: "Videos",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_uploaded_by",
                table: "Videos",
                column: "uploaded_by");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Lifes");

            migrationBuilder.DropTable(
                name: "Stars");

            migrationBuilder.DropTable(
                name: "Comics");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
