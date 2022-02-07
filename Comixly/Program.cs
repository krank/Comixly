global using HtmlAgilityPack;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

// Step 1: Filenames
string[] files = Directory.GetFiles("html");

// Step 2: Instances of ComixologyFile
List<ComixologyFile> comixologyFiles = new List<ComixologyFile>();

foreach (string fileName in files)
{
  comixologyFiles.Add(new ComixologyFile(fileName));
}

// Step 3: ComicBook records
List<ComicBook> comicBooks = new List<ComicBook>();

foreach (ComixologyFile comixologyFile in comixologyFiles)
{
  comicBooks.AddRange(comixologyFile.GetBooks());
}
Console.WriteLine($"Total length of list: {comicBooks.Count}");

// Step 4: Write to CSV

using FileStream fileStream = File.Create("comixRecords.csv");
using TextWriter textWriter = new StreamWriter(fileStream);
using var csvWriter = new CsvWriter(textWriter, CultureInfo.InvariantCulture, false);

csvWriter.WriteHeader<ComicBook>();
csvWriter.NextRecord();
csvWriter.WriteRecords<ComicBook>(comicBooks);


Console.ReadLine();
