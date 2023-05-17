
using NasdaqOmxNordicParser.Models;
using Serilog;
using Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace NasdaqOmxNordicParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string date = DateTime.Now.ToShortDateString().ToString().Replace(".", "");
            string baseUrl = "https://www.nasdaqomxnordic.com/";
            string contentUrl = "https://www.nasdaqomxnordic.com/optionsandfutures/microsite?Instrument=SE0000337842";
            string filePath = $"{date}.csv";
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File("logKrxNosdaqParser.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
                

            try {
                var res = GetData(baseUrl, contentUrl);
                PutDataToFile(res, filePath);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); } 
            
        }
    
        
        private static OptionData GetData(string baseUrl, string contentUrl)
        {
            
            var proxy = new WebProxy("127.0.0.1:8888");
            var cookieContainer = new CookieContainer();

            Log.Information("Getting HTML content from {Url}", baseUrl);
            var getRequest = new GetRequest($"{baseUrl}");
            getRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7";
            getRequest.Useragent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 YaBrowser/23.5.0.2199 Yowser/2.5 Safari/537.36";
            //getRequest.Headers.Add("sec-ch-ua", "\"Chromium\";v=\"92\", \" Not A;Brand\";v=\"99\", \"Google Chrome\";v=\"92\"");
            //getRequest.Headers.Add("sec-ch-ua-mobile", "?0");
            //getRequest.Headers.Add("Sec-Fetch-Dest", "document");
            //getRequest.Headers.Add("Sec-Fetch-Mode", "navigate");
            //getRequest.Headers.Add("Sec-Fetch-Site", "same-origin");
            //getRequest.Headers.Add("Upgrade-Insecure-Requests", "1");
            getRequest.Host = "www.nasdaqomxnordic.com";
            getRequest.Proxy = proxy;
            getRequest.Run(cookieContainer);
            Log.Information("Done with GET");


            Log.Information("Getting HTML content from {Url}", contentUrl);
            var postrequest = new PostRequest($"{contentUrl}");
            postrequest.Accept = "*/*";
            postrequest.Useragent = "mozilla/5.0 (windows nt 10.0; win64; x64) applewebkit/537.36 (khtml, like gecko) chrome/92.0.4515.159 safari/537.36";
            postrequest.ContentType = "application/x-www-form-urlencoded";
            postrequest.Referer = baseUrl;
            postrequest.Host = baseUrl;
            postrequest.Proxy = proxy;

            //postrequest.headers.add("bx-ajax", "true");
            //postrequest.headers.add("origin", "https://www.nasdaqomxnordic.com/");
            //postrequest.headers.add("sec-ch-ua", "\"chromium\";v=\"92\", \" not a;brand\";v=\"99\", \"google chrome\";v=\"92\"");
            //postrequest.headers.add("sec-ch-ua-mobile", "?0");
            //postrequest.headers.add("sec-fetch-dest", "empty");
            //postrequest.headers.add("sec-fetch-mode", "cors");
            //postrequest.headers.add("sec-fetch-site", "same-origin");

            //postrequest.run(cookiecontainer);
            OptionData data = new OptionData();

            Log.Information("Done");

            return data;
        }
        private static void PutDataToFile(object resultStat, string filePath)
        {
            var writer = new StreamWriter(filePath);
            Log.Information("Writing data to CSV file {FilePath}", filePath);
            writer.WriteLine($"{resultStat}");
        }
    }
}