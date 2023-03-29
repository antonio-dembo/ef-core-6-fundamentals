// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

PubContext _context = new PubContext();
//this assumes you are working with the populated
//database created in privious module

QueryFilters();

void QueryFilters()
{
    string name = "Josie";
    var authors = _context.Authors.Where( s => s.FirstName == name).ToList();
}
