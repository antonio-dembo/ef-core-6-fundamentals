// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

PubContext _context = new PubContext();
//this assumes you are working with the populated
//database created in previous module

//InsertAuthor();
CoordinatedRetrieveAndUpdateAuthor();

void CoordinatedRetrieveAndUpdateAuthor()
{
    var author = FindThatAuthor(3);
    if(author?.FirstName == "Julie")
    {
        author.FirstName = "Julia";
        SaveThatChanges(author);
    }
}

Author FindThatAuthor(int authorId)
{
    using var shortLivedContext = new PubContext();
    return shortLivedContext.Authors.Find(authorId);
}

void SaveThatChanges(Author author)
{
    using var anotherShortLivedContext = new PubContext();
    anotherShortLivedContext.Authors.Update(author);
    anotherShortLivedContext.SaveChanges();
}

void InsertAuthor()
{
    //var author = new Author { FirstName= "Frank", LastName = "Herbert" };

    _context.Authors.Add(new Author { FirstName = "Dan", LastName = "Apple" });
    _context.Authors.Add(new Author { FirstName = "Ruth", LastName = "Ozeki" });
    _context.Authors.Add(new Author { FirstName = "Sofia", LastName = "Segovia" });
    _context.Authors.Add(new Author { FirstName = "Ursula K.", LastName = "LeGuin" });
    _context.Authors.Add(new Author { FirstName = "Hugh", LastName = "Howey" });
    _context.Authors.Add(new Author { FirstName = "Isabelle", LastName = "Allende" });
    _context.SaveChanges();
}