using ApiDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiDemo.Persistence
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
            : base(options) { }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        public DbSet<MovementItemAggregate> MovementItemAggregates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Book mappings
            modelBuilder.Entity<Book>()
                .Property(b => b.Title)
                .IsRequired();    
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .IsRequired();
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Reviews)
                .WithOne(r => r.Book)
                .OnDelete(DeleteBehavior.Cascade);    

            // Review mappings
            modelBuilder.Entity<Review>()
                .Property(r => r.ReviewerName)
                .IsRequired(); 
            modelBuilder.Entity<Review>()
                .Property(r => r.Body)
                .IsRequired();     
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Reviews)
                .IsRequired();    

            // Author mappings
            modelBuilder.Entity<Author>()
                .Property(a => a.FirstName)
                .IsRequired();    
        }
    }
}