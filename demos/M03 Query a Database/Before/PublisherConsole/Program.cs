// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

PubContext _context = new PubContext();
//this assumes you are working with the populated
//database created in privious module

QueryFilters();
//FindIt();
//AddSomeMoreAuthors();
//SkipAndTakeAuthors();
//SortAuthors();

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