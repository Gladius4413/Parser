using System;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            var resultStat = GetData("http://data.krx.co.kr/comm/menu/menuLoader/getMdcMenu.cmd");
        }

    }

     private static object GetData(string url)
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
                    clnt.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:80.0) Gecko/20100101 Firefox/80.0");
                    clnt.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                    clnt.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
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
                    clnt.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:80.0) Gecko/20100101 Firefox/80.0");
                    clnt.DefaultRequestHeaders.Add("Accept", "application/json, text/javascript, */*; q=0.01");
                    clnt.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
                    clnt.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                    clnt.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    clnt.DefaultRequestHeaders.Add("Referer", url);

                    using (var resp = clnt.GetAsync($"https://www.banki.ru/moex/iss/engines/currency/markets/selt/securities/USD000UTSTOM/candles.json?from={DateTime.Now.AddDays(-2):yyyy-MM-dd}&interval=1&start=0").Result)
                    {
                        if (resp.IsSuccessStatusCode)
                        {
                            var json = resp.Content.ReadAsStringAsync().Result;
                            if (!string.IsNullOrEmpty(json))
                            {
                                BankiRuRespons result = Newtonsoft.Json.JsonConvert.DeserializeObject<BankiRuRespons>(json);
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