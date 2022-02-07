using System;

public class ComixologyFile
{
  private HtmlDocument htmlDocument;
  private string fileName;

  public ComixologyFile(string fileName)
  {
    this.fileName = fileName;
    htmlDocument = new HtmlDocument();
    htmlDocument.Load(fileName);
  }

  public List<ComicBook> GetBooks()
  {
    List<ComicBook> comicBooks = new List<ComicBook>();

    HtmlNodeCollection nodes = htmlDocument.DocumentNode.SelectNodes("//article[contains(@class, 'lv2-book')]");

    foreach(HtmlNode node in nodes)
    {
      // Find title
      HtmlNode titleNode = node.SelectSingleNode(".//div[@class='lv2-micro-item-title']");
      string title = titleNode != null ? titleNode.InnerText.Trim() : "";

      // Find subtitle
      HtmlNode subtitleNode = node.SelectSingleNode(".//div[@class='lv2-micro-item-subtitle']");
      string subtitle = subtitleNode != null ? subtitleNode.InnerText.Trim() : "";

      // Find DRM
      HtmlNode drmNode = node.SelectSingleNode(".//img[@title='DRM-Free']");
      bool drmFree = drmNode != null;

      comicBooks.Add(new ComicBook(title, subtitle, drmFree));

      System.Console.WriteLine($" Added {title} - {subtitle}");
    }

    Console.WriteLine("--------------------------------------------------------------------------------");
    Console.WriteLine($"Processed {fileName}");
    Console.WriteLine($"  {comicBooks.Count} books");
    Console.WriteLine("================================================================================");

    return comicBooks;
  }

}