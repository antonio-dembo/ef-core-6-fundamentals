﻿using Microsoft.EntityFrameworkCore;
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
                new Author { AuthorId = 1, FirstName ="Rhoda", LastName = "Lerman"});

            var authorList = new List<Author>
            {
                new Author { AuthorId= 2, FirstName = "Ruth", LastName = "OZeki" },
                new Author { AuthorId= 3, FirstName = "Sofia", LastName = "Segovia" },
                new Author { AuthorId= 4, FirstName = "Ursula K.", LastName = "Howey" },
                new Author { AuthorId= 5, FirstName = "Hugh", LastName = "Haunts" },
                new Author { AuthorId= 6, FirstName = "Isabelle", LastName = "Allende" },
            };

            modelBuilder.Entity<Author>().HasData(authorList);

            var someBooks = new List<Book> {
                new Book { BookId= 1, AuthorId=1, Title = "In God's Ear", PublishDate = new DateTime(1989,3,1)},
                new Book { BookId= 2, AuthorId=2, Title = "A Tale For the Time Being", PublishDate = new DateTime(2013,12,31)},
                new Book { BookId= 3, AuthorId=3, Title = "The Left Hand of Darkness", PublishDate = new DateTime(1961,3,1)},
            };
            modelBuilder.Entity<Book>().HasData(someBooks);

            //example of more advenced mapping to specify
            //a one-to-many between author and book when
            //there are no navigation propperties
            //modelBuilder.Entity<Author>()
            //    .HasMany<Book>()
            //    .WithOne();
        }
    }
}