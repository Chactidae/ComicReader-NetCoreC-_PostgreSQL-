using ComicReader.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicReader.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Image> Images { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships (optional, but recommended)
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Images)
                .WithOne(i => i.Book)
                .HasForeignKey(i => i.Id);
        }*/
    }
}
