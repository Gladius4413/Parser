
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

internal class Program
{
    

    private static void Main(string[] args)
    {
        string date = DateTime.Now.ToShortDateString().ToString().Replace(".","");
        string url = "http://data.krx.co.kr/contents/MDC/MDI/mdiLoader/index.cmd?menuId=MDC0201";
            string filePath = $"{date}.csv";

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("logKrxParser.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        try
        {
            Log.Information("Getting JSON content from {Url}", url);
            var resultStat = GetData(url);
            PutDataToFile(resultStat, filePath);
            Log.Information("Done");


        }
        catch(Exception e) { Console.WriteLine(e.Message); }


    }

    private static void PutDataToFile(object resultStat, string filePath)
    {
        var writer = new StreamWriter(filePath);
        Log.Information("Writing data to CSV file {FilePath}", filePath);
        writer.WriteLine($"{resultStat}");
    }

    private static KrxData GetData(string url)
    {
        try
        {
            using (HttpClientHandler hdl = new HttpClientHandler
            {
                AllowAutoRedirect = false,
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate | System.Net.DecompressionMethods.None,
                CookieContainer = new System.Net.CookieContainer()
            })
            {
                using (HttpClient clnt = new HttpClient(hdl, false))
                {
                    clnt.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 YaBrowser/23.5.0.2199 Yowser/2.5 Safari/537.36");
                    clnt.DefaultRequestHeaders.Add("Accept", "application/json, text/javascript, */*; q=0.01");
                    clnt.DefaultRequestHeaders.Add("Accept-Language", "ru,en;q=0.9");
                    //clnt.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
                    clnt.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    clnt.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");

                    using (var resp = clnt.GetAsync(url).Result)
                    {
                        if (!resp.IsSuccessStatusCode)
                        {
                            return null;
                        }
                    }
                }

                using (HttpClient clnt = new HttpClient(hdl, false))
                {
                    clnt.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 YaBrowser/23.5.0.2199 Yowser/2.5 Safari/537.36");
                    clnt.DefaultRequestHeaders.Add("Accept", "application/json, text/javascript, */*; q=0.01");
                    clnt.DefaultRequestHeaders.Add("Accept-Language", "ru,en;q=0.9");
                    clnt.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                    clnt.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    clnt.DefaultRequestHeaders.Add("Referer", url);

                    using (var resp = clnt.GetAsync("http://data.krx.co.kr/comm/menu/menuLoader/getMdcMenu.cmd")) 
                    {
                        {
                            var json = resp.Content.ReadAsStringAsync().Result;
                            if (!string.IsNullOrEmpty(json))
                            {
                                KrxData result = Newtonsoft.Json.JsonConvert.DeserializeObject<KrxData>(json);
                                return result;
                            }
                    }
                }

            }
          }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }

        return null;
    }
}