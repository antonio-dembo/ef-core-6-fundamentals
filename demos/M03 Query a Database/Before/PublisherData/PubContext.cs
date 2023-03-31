using Microsoft.EntityFrameworkCore;
using PublisherDomain;

namespace PublisherData
{
    public class PubContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
              "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = PubDatabase"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, FirstName ="Rhoda", LastName = "Lerman"});

            var newAuthors = new List<Author>
            {
                new Author { Id= 2, FirstName = "Ruth", LastName = "OZeki" },
                new Author { Id= 3, FirstName = "Sofia", LastName = "Segovia" },
                new Author { Id= 4, FirstName = "Ursula K.", LastName = "Howey" },
                new Author { Id= 5, FirstName = "Hugh", LastName = "Haunts" },
                new Author { Id= 6, FirstName = "Isabelle", LastName = "Allende" },
            };

            modelBuilder.Entity<Author>().HasData(newAuthors );
        }
    }
}