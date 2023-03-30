// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

PubContext _context = new PubContext();
//this assumes you are working with the populated
//database created in privious module

//QueryFilters();
//FindIt();
//AddSomeMoreAuthors();
//SkipAndTakeAuthors();
//SortAuthors();
//QueryAggregate();
//RetrieveAndUpdateAuthor();
//RetrieveAndUpdateMultipleAuthor();
//VariousOperations();
CoordinatedRetrieveAndUpdateAuthor();

void CoordinatedRetrieveAndUpdateAuthor()
{
    var author = FindThatAuthor(3);
    if (author?.FirstName == "Julie")
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

void VariousOperations()
{
    var author = _context.Authors.Find(2); // this is currently Josie Newf
    author.LastName = "Newfoundland";

    var newauthor = new Author { LastName = "Appleman", FirstName = "Dan" };
    _context.Authors.Add(newauthor);
    _context.SaveChanges();
}

void RetrieveAndUpdateMultipleAuthor()
{
    var lermanAuthors = _context.Authors.Where(a => a.LastName == "Lehrman").ToList();
    foreach (var la in lermanAuthors)
    {
        la.LastName = "Lerman";
    }

    Console.WriteLine("Before: " + _context.ChangeTracker.DebugView.ShortView);
    _context.ChangeTracker.DetectChanges();
    Console.WriteLine("After:" + _context.ChangeTracker.DebugView.ShortView);

    _context.SaveChanges(); // calls DetectChanges() again as this is what it does.
}

void RetrieveAndUpdateAuthor()
{
    var author = _context.Authors.FirstOrDefault(a => a.FirstName == "Julie" && a.LastName == "Lerman");
    if(author != null)
    {
        author.LastName = "Lehrman";
    }
    _context.SaveChanges(); // This method calls DetectChanges() internally.
}

void QueryAggregate()
{
    //var author = _context.Authors.OrderByDescending(a => a.FirstName)
    //    .FirstOrDefault(a => a.LastName == "Lerman");

    //Throws an exception in runtime.
    //Last methods require query to have an OrderBy() method
    //otherwise will throw an exception.
    //the compiler can't warn you, because query translation happens at runtime.
    //var auth = _context.Authors.LastOrDefault(a => a.LastName == "Lerman"); //Error
    var auth = _context.Authors.OrderBy(a=> a.FirstName)
        .LastOrDefault(a => a.LastName == "Lerman");
}

void SortAuthors()
{
    var authorsByLastName = _context.Authors
        .OrderBy(a => a.LastName)
        .ThenBy(a => a.FirstName).ToList();
    authorsByLastName.ForEach(a => Console.WriteLine(a.LastName + ","+ a.FirstName));

    var authorsDescending = _context.Authors
        .OrderByDescending(a => a.LastName)
        .ThenByDescending(a => a.FirstName).ToList();
    Console.WriteLine("**Descending Last and First**");
    authorsDescending.ForEach(a => Console.WriteLine(a.LastName + ","+ a.FirstName));


}

void AddSomeMoreAuthors() 
{
    _context.Authors.Add(new Author { FirstName = "Rhoda", LastName = "Lerman" });
    _context.Authors.Add(new Author { FirstName = "Don", LastName = "Jones" });
    _context.Authors.Add(new Author { FirstName = "Jim", LastName = "Christopher" });
    _context.Authors.Add(new Author { FirstName = "Stephen", LastName = "Haunts" });
    _context.SaveChanges();
}

void SkipAndTakeAuthors()
{
    int groupSize = 2;
    for (int i = 0; i < 5; i++)
    {
        var authors = _context.Authors.Skip(groupSize * i).Take(groupSize).ToList();
        Console.WriteLine($"Group {i}:");
        foreach (var author in authors)
        {
            Console.WriteLine($" {author.FirstName} {author.LastName}");
        }
    }
}

void QueryFilters()
{
    //string name = "Josie";
    //var authors = _context.Authors.Where( s => s.FirstName == name).ToList();
    string filter = "L%";
    var authors = _context.Authors
        .Where(s => EF.Functions.Like(s.LastName, filter))
        .OrderByDescending(a => a.FirstName).ToList();
    authors.ForEach(a => Console.WriteLine(a.LastName +","+ a.FirstName));
}

void FindIt()
{
    var authorIdTwo = _context.Authors.Find(2);
}