using System.Collections.Generic;
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ProjectSeraph.Logic
{
    class SiteSearch
    {

        HttpClient httpClient = new HttpClient();
        HttpResponseMessage site;
        HtmlNodeCollection pre;
        HtmlDocument doc = new HtmlDocument();
        IEnumerable<HtmlAgilityPack.HtmlNode> links;
        ArrayList hrefs = new ArrayList();
        string siteString;

        public SiteSearch()
        {}

        public async Task<string> pph()
        {

            System.Console.WriteLine("Class SiteSearch: Start");

            site = await httpClient.GetAsync("https://www.peopleperhour.com/freelance-jobs");
            siteString = await site.Content.ReadAsStringAsync();

            System.Console.WriteLine("HAP: Start");

            doc.LoadHtml(siteString);
            pre = doc.DocumentNode.SelectNodes("//div[contains(@class, 'main-content full-width')]//div[contains(@class, 'clearfix listing-row project-list-item job-list-item ')]");

            System.Console.WriteLine("HAP: precount {0}", pre.Count);

            links = pre.Descendants("a");

            foreach(var node in links){
                hrefs.Add(node.GetAttributeValue("title", string.Empty));
            }

            for (int i = 0; i < hrefs.Count; i++)
            {
                System.Console.WriteLine("HAP: {0}", hrefs[i]);
            }
                

            System.Console.WriteLine("HAP: Finish");
            System.Console.WriteLine("Class SiteSearch: return: site");
            return siteString;
        }

    }
}