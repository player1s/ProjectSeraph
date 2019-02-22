using System;
using System.Collections;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ProjectSeraph.Logic
{
    class SiteSearch
    {
        public SiteSearch()
        {}

        //public string responseString { get; set; }

        public async Task<string> pph()
        {
            System.Console.WriteLine("Class SiteSearch: Start");

            var httpClient = new HttpClient();
            var site = await httpClient.GetAsync("https://www.peopleperhour.com/freelance-jobs");

            string responseString = await site.Content.ReadAsStringAsync();

            System.Console.WriteLine("Regexing: Start");
            var doc = new HtmlDocument();
            doc.LoadHtml(@"<!DOCTYPE HTML PUBLIC ""-//IETF//DTD HTML//EN"">
                    <HTML>
                    <HEAD>
                    <TITLE>FTP </TITLE>
                    <BASE HREF="">
                    </HEAD>
                    <BODY>
                    <H2>FTP Listing of </H2>
                    <HR>
                    <A HREF=""../"">Parent Directory</A><BR>
                    <PRE>
                    Jul 25 2014 08:47       254433 <A HREF=""_UGV_UK_day_comparisons_new_ms_20140601-20140721.csv"">_UGV_UK_day_comparisons_new_ms_20140601-20140721.csv</A>
                    Sep 02 2014 15:04       225482 <A HREF=""CH_UGV_day_1_minute_20140901.zip"">CH_UGV_day_1_minute_20140901.zip</A>
                    Sep 03 2014 15:03       213326 <A HREF=""CH_UGV_day_1_minute_20140902.zip"">CH_UGV_day_1_minute_20140902.zip</A>
                    Sep 04 2014 15:05       207920 <A HREF=""CH_UGV_day_1_minute_20140903.zip"">CH_UGV_day_1_minute_20140903.zip</A>

                    </PRE>
                    <PRE>
                    Jul 25 2014 08:47       254433 <A HREF=""_UGV_UK_day_comparisons_new_ms_20140601-20140721.csv"">_UGV_UK_day_comparisons_new_ms_20140601-20140721.csv</A>
                    </PRE>
                    <HR>
                    </BODY>
                    </HTML>");

                //var pre = doc.DocumentNode.SelectNodes("//div[contains(@class, 'hello')]");
                var pre = doc.DocumentNode.Descendants("pre").FirstOrDefault();
                var links = pre.Descendants("a");
                ArrayList hrefs = new ArrayList();
                foreach(var node in links){
                    hrefs.Add(node.GetAttributeValue("href", string.Empty));
                }

                for (int i = 0; i < hrefs.Count; i++)
                {
                    System.Console.WriteLine("HAP: {0}", hrefs[i]);
                }
                

            System.Console.WriteLine("Regexing: Finish");
            System.Console.WriteLine("Class SiteSearch: return: site");
            return responseString;
        }

    }
}