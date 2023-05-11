using CsvHelper;
using HtmlAgilityPack;
using NasdaqOmxNordicParser.Models;
using Serilog;
using System.Net;
using System.Text;


 class Program
{
    private static readonly HttpClient httpClient = new HttpClient();
    private static readonly string url = "https://www.nasdaqomxnordic.com/optionsandfutures/microsite?Instrument=SE0000337842";
    private static readonly string filePath = @"data.csv";

    static void Main(string[] args)
    {
       
    // Configure Serilog logging
    Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        try
        {
            // Get HTML content
            Log.Information("Getting HTML content from {Url}", url);
            var response =  httpClient.GetAsync(url).Result;
            var content =  response.Content.ReadAsStringAsync().Result;

            // Load HTML content into HtmlDocument
            Log.Information("Parsing HTML content");
            var doc = new HtmlDocument();
            doc.LoadHtml(content);

            // Get table rows and write to CSV file
            Log.Information("Writing data to CSV file {FilePath}", filePath);
            using (var writer = new StreamWriter(filePath))
            {
                foreach (var row in doc.DocumentNode.SelectNodes("//table[contains(@class,'table-options')]/tbody/tr"))
                {
                    var cells = row.SelectNodes("td");
                    var expirationDate = cells[0].InnerText.Trim();
                    var strikePrice = cells[1].InnerText.Trim();
                    var bidPrice = cells[2].InnerText.Trim();
                    var askPrice = cells[3].InnerText.Trim();
                    var volume = cells[4].InnerText.Trim();
                    var openInterest = cells[5].InnerText.Trim();

                    writer.WriteLine($"{expirationDate},{strikePrice},{bidPrice},{askPrice},{volume},{openInterest}");
                }
            }

            // Save data to database
            Log.Information("Saving data to database");
            // TODO: Implement database saving logic

            Log.Information("Done");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while parsing data");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}