using Microsoft.EntityFrameworkCore;
using Net9RestApi.Models;

namespace Net9RestApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Bu DbSet özellikleri veritabanındaki tablolarımız olacak
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // --- İlişki Ayarları (Fluent API) ---
            
            // 1. User -> Articles (Bire-Çok İlişki)
            modelBuilder.Entity<Article>()
                .HasOne(a => a.User)
                .WithMany(u => u.Articles)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Yazar silinirse makaleleri de silinsin.

            // 2. Category -> Articles (Bire-Çok İlişki)
            modelBuilder.Entity<Article>()
                .HasOne(a => a.Category)
                .WithMany()
                .HasForeignKey(a => a.CategoryId);

            // 3. Article -> Comments (Bire-Çok İlişki)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Article)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.ArticleId)
                .OnDelete(DeleteBehavior.Cascade); // Makale silinirse yorumları da silinsin.
        }
    }
}