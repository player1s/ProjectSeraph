using System.Collections;
using System.Net.Http;
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

            System.Console.WriteLine("HAP: Start");
            var doc = new HtmlDocument();
            doc.LoadHtml(responseString);

                var pre = doc.DocumentNode.SelectNodes("//div[contains(@class, 'main-content full-width')]//div[contains(@class, 'clearfix listing-row project-list-item job-list-item ')]");

                System.Console.WriteLine("HAP: precount {0}", pre.Count);

                var links = pre.Descendants("a");

                ArrayList hrefs = new ArrayList();
                foreach(var node in links){
                    hrefs.Add(node.GetAttributeValue("title", string.Empty));
                }

                for (int i = 0; i < hrefs.Count; i++)
                {
                    System.Console.WriteLine("HAP: {0}", hrefs[i]);
                }
                

            System.Console.WriteLine("HAP: Finish");
            System.Console.WriteLine("Class SiteSearch: return: site");
            return responseString;
        }

    }
}