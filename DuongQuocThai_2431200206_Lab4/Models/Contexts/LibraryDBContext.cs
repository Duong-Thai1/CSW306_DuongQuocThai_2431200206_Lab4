using Microsoft.EntityFrameworkCore;

namespace DuongQuocThai_2431200206_Lab4.Models.Contexts
{
    public class LibraryDBContext : DbContext
    {
        public LibraryDBContext(DbContextOptions<LibraryDBContext> options) : base(options)
        {

        }

        public DbSet<Book> Books {  get; set; }
        public DbSet<Author> Authors {  get; set; }
        public DbSet<User> Users {  get; set; }
        public DbSet<Category> Categories {  get; set; }
        public DbSet<Loan> Loans {  get; set; }

        public DbSet<Carousel> Carousels { get; set;  }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Book>()
        //        .HasMany(e => e.Categories)
        //        .WithMany(e => e.Books)
        //        .HasForeignKey(e => e.CategoryId)
        //        .IsRequired();
        //}

    }
}
