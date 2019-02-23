using System.Collections.Generic;
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using ProjectSeraph.model;

namespace ProjectSeraph.Logic
{
    class SiteSearch
    {
        HttpClient httpClient = new HttpClient();
        HttpResponseMessage site;
        HtmlNodeCollection preLinks;
        HtmlNodeCollection preTime;
        HtmlDocument doc = new HtmlDocument();
        IEnumerable<HtmlAgilityPack.HtmlNode> links;
        IEnumerable<HtmlAgilityPack.HtmlNode> time;
        string siteString;
        List<string> titles = new List<string>();
        List<string> urls = new List<string>();
        List<Job> pphJobs = new List<Job>();

        public SiteSearch()
        {}

        public async Task<string> pph()
        {

            System.Console.WriteLine("Class SiteSearch: Start");

            site = await httpClient.GetAsync("https://www.peopleperhour.com/freelance-jobs");
            siteString = await site.Content.ReadAsStringAsync();

            System.Console.WriteLine("HAP: Start");

            doc.LoadHtml(siteString);
            preLinks = doc.DocumentNode.SelectNodes("//div[contains(@class, 'main-content full-width')]//h6[contains(@class, 'title')]");
            preTime = doc.DocumentNode.SelectNodes("//div[contains(@class, 'main-content full-width')]//ul[contains(@class, 'clearfix member-info horizontal crop hidden-xs')]");

            System.Console.WriteLine("HAP: precount {0}", preLinks.Count);
            
            links = preLinks.Descendants("a");
            time = preTime.Descendants("time");

            foreach(var node in links){
                
                Job job = new Job();

                job.Title = node.GetAttributeValue("title", string.Empty);
                job.URL = node.GetAttributeValue("href", string.Empty);

                pphJobs.Add(job);
            }

           for (int i = 0; i < pphJobs.Count; i++)
           {
                System.Console.WriteLine("HAP: in List : title: {0}",pphJobs[i].Title);
           }     

            System.Console.WriteLine("HAP: Finish");
            System.Console.WriteLine("Class SiteSearch: return: site");
            return siteString;
        }

    }
}