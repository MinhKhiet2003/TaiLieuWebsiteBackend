using Microsoft.EntityFrameworkCore;
using TaiLieuWebsiteBackend.Models;

namespace TaiLieuWebsiteBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Star> Stars { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Thiết lập các mối quan hệ và ràng buộc
            modelBuilder.Entity<Category>()
                .HasOne(v => v.Class)
                .WithMany(c => c.Categories)
                .HasForeignKey(v => v.class_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Document>()
                .HasOne(d => d.Category)
                .WithMany(c => c.Documents)
                .HasForeignKey(d => d.category_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Video>()
                .HasOne(v => v.Category)
                .WithMany(c => c.Videos)
                .HasForeignKey(v => v.category_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.Category)
                .WithMany(c => c.Games)
                .HasForeignKey(g => g.category_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Exercise>()
                .HasOne(e => e.Category)
                .WithMany(c => c.Exercises)
                .HasForeignKey(e => e.category_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.user_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
               .HasOne(c => c.Document)
               .WithMany(v => v.Comments)
               .HasForeignKey(c => c.document_id)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
               .HasOne(c => c.Game)
               .WithMany(v => v.Comments)
               .HasForeignKey(c => c.game_id)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
               .HasOne(c => c.Video)
               .WithMany(v => v.Comments)
               .HasForeignKey(c => c.video_id)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Star>()
                .HasOne(c => c.User)
               .WithMany()
                .HasForeignKey(c => c.star_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Star>()
               .HasOne(c => c.Document)
               .WithMany(v => v.Stars)
               .HasForeignKey(c => c.star_id)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Star>()
               .HasOne(c => c.Exercise)
               .WithMany(v => v.Stars)
               .HasForeignKey(c => c.star_id)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Star>()
               .HasOne(c => c.Game)
               .WithMany(v => v.Stars)
               .HasForeignKey(c => c.star_id)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Star>()
               .HasOne(c => c.Video)
               .WithMany(v => v.Stars)
               .HasForeignKey(c => c.star_id)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
