
using Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NasdaqOmxNordicParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var proxy = new WebProxy("127.0.0.1:8888");
            var cookieContainer = new CookieContainer();

            var getRequest = new GetRequest($"https://www.nasdaqomxnordic.com/");
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


            //var postrequest = new postrequest("https://baucenter.ru/");
            //postrequest.data = $"ajax_call=y&input_id=title-search-input&q={code}&l=2";
            //postrequest.accept = "*/*";
            //postrequest.useragent = "mozilla/5.0 (windows nt 10.0; win64; x64) applewebkit/537.36 (khtml, like gecko) chrome/92.0.4515.159 safari/537.36";
            //postrequest.contenttype = "application/x-www-form-urlencoded";
            //postrequest.referer = "https://baucenter.ru/";
            //postrequest.host = "baucenter.ru";
            //postrequest.proxy = proxy;

            //postrequest.headers.add("bx-ajax", "true");
            //postrequest.headers.add("origin", "https://baucenter.ru");
            //postrequest.headers.add("sec-ch-ua", "\"chromium\";v=\"92\", \" not a;brand\";v=\"99\", \"google chrome\";v=\"92\"");
            //postrequest.headers.add("sec-ch-ua-mobile", "?0");
            //postrequest.headers.add("sec-fetch-dest", "empty");
            //postrequest.headers.add("sec-fetch-mode", "cors");
            //postrequest.headers.add("sec-fetch-site", "same-origin");

            //postrequest.run(cookiecontainer);



            Console.ReadKey();
        }
    }
}