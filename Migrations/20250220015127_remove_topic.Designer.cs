﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaiLieuWebsiteBackend.Data;

#nullable disable

namespace TaiLieuWebsiteBackend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250220015127_remove_topic")]
    partial class remove_topic
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TaiLieuWebsiteBackend.Models.Category", b =>
                {
                    b.Property<int>("category_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("category_id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.Property<int>("class_id")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("category_id");

                    b.HasIndex("class_id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("TaiLieuWebsiteBackend.Models.Class", b =>
                {
                    b.Property<int>("class_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("class_id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("class_id");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("TaiLieuWebsiteBackend.Models.Comment", b =>
                {
                    b.Property<int>("comment_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("comment_id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("document_id")
                        .HasColumnType("int");

                    b.Property<int>("game_id")
                        .HasColumnType("int");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.Property<int>("video_id")
                        .HasColumnType("int");

                    b.HasKey("comment_id");

                    b.HasIndex("document_id");

                    b.HasIndex("game_id");

                    b.HasIndex("user_id");

                    b.HasIndex("video_id");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("TaiLieuWebsiteBackend.Models.Document", b =>
                {
                    b.Property<int>("document_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("document_id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.Property<int>("category_id")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("file_path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("uploaded_by")
                        .HasColumnType("int");

                    b.HasKey("document_id");

                    b.HasIndex("category_id");

                    b.HasIndex("uploaded_by");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("TaiLieuWebsiteBackend.Models.Exercise", b =>
                {
                    b.Property<int>("exercise_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("exercise_id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.Property<int>("category_id")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("difficulty")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("uploaded_by")
                        .HasColumnType("int");

                    b.HasKey("exercise_id");

                    b.HasIndex("category_id");

                    b.HasIndex("uploaded_by");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("TaiLieuWebsiteBackend.Models.Game", b =>
                {
                    b.Property<int>("game_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("game_id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.Property<int>("category_id")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("game_url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("uploaded_by")
                        .HasColumnType("int");

                    b.HasKey("game_id");

                    b.HasIndex("category_id");

                    b.HasIndex("uploaded_by");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("TaiLieuWebsiteBackend.Models.Star", b =>
                {
                    b.Property<int>("star_id")
                        .HasColumnType("int");

                    b.Property<int>("document_id")
                        .HasColumnType("int");

                    b.Property<int>("exercise_id")
                        .HasColumnType("int");

                    b.Property<int>("game_id")
                        .HasColumnType("int");

                    b.Property<int>("total_star")
                        .HasColumnType("int");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.Property<int>("video_id")
                        .HasColumnType("int");

                    b.HasKey("star_id");

                    b.ToTable("Stars");
                });

            modelBuilder.Entity("TaiLieuWebsiteBackend.Models.User", b =>
                {
                    b.Property<int>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("user_id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<string>("ProfilePicturePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password_hash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("user_id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TaiLieuWebsiteBackend.Models.Video", b =>
                {
                    b.Property<int>("video_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("video_id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.Property<int>("category_id")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("uploaded_by")
                        .HasColumnType("int");

                    b.Property<string>("video_url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("video_id");

                    b.HasIndex("category_id");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("TaiLieuWebsiteBackend.Models.Category", b =>
                {
                    b.HasOne("TaiLieuWebsiteBackend.Models.Class", "Class")
                        .WithMany("Categories")
                        .HasForeignKey("class_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");
                });

            modelBuilder.Entity("TaiLieuWebsiteBackend.Models.Comment", b =>
                {
                    b.HasOne("TaiLieuWebsiteBackend.Models.Document", "Document")
                        .WithMany("Comments")
                        .HasForeignKey("document_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TaiLieuWebsiteBackend.Models.Game", "Game")
                        .WithMany("Comments")
                        .HasForeignKey("game_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TaiLieuWebsiteBackend.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TaiLieuWebsiteBackend.Models.Video", "Video")
                        .WithMany("Comments")
                        .HasForeignKey("video_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("Game");

                    b.Navigation("User");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("TaiLieuWebsiteBackend.Models.Document", b =>
                {
                    b.HasOne("TaiLieuWebsiteBackend.Models.Category", "Category")
                        .WithMany("Documents")
                        .HasForeignKey("category_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaiLieuWebsiteBackend.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("uploaded_by")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaiLieuWebsiteBackend.Models.Exercise", b =>
                {
                    b.HasOne("TaiLieuWebsiteBackend.Models.Category", "Category")
                        .WithMany("Exercises")
                        .HasForeignKey("category_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaiLieuWebsiteBackend.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("uploaded_by")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaiLieuWebsiteBackend.Models.Game", b =>
                {
                    b.HasOne("TaiLieuWebsiteBackend.Models.Category", "Category")
                        .WithMany("Games")
                        .HasForeignKey("category_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaiLieuWebsiteBackend.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("uploaded_by")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaiLieuWebsiteBackend.Models.Star", b =>
                {
                    b.HasOne("TaiLieuWebsiteBackend.Models.Document", "Document")
                        .WithMany("Stars")
                        .HasForeignKey("star_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TaiLieuWebsiteBackend.Models.Exercise", "Exercise")
                        .WithMany("Stars")
                        .HasForeignKey("star_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TaiLieuWebsiteBackend.Models.Game", "Game")
                        .WithMany("Stars")
                        .HasForeignKey("star_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TaiLieuWebsiteBackend.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("star_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TaiLieuWebsiteBackend.Models.Video", "Video")
                        .WithMany("Stars")
                        .HasForeignKey("star_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("Exercise");

                    b.Navigation("Game");

                    b.Navigation("User");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("TaiLieuWebsiteBackend.Models.Video", b =>
                {
                    b.HasOne("TaiLieuWebsiteBackend.Models.Category", "Category")
                        .WithMany("Videos")
                        .HasForeignKey("category_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("TaiLieuWebsiteBackend.Models.Category", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("Exercises");

                    b.Navigation("Games");

                    b.Navigation("Videos");
                });

            modelBuilder.Entity("TaiLieuWebsiteBackend.Models.Class", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("TaiLieuWebsiteBackend.Models.Document", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Stars");
                });

            modelBuilder.Entity("TaiLieuWebsiteBackend.Models.Exercise", b =>
                {
                    b.Navigation("Stars");
                });

            modelBuilder.Entity("TaiLieuWebsiteBackend.Models.Game", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Stars");
                });

            modelBuilder.Entity("TaiLieuWebsiteBackend.Models.Video", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Stars");
                });
#pragma warning restore 612, 618
        }
    }
}
