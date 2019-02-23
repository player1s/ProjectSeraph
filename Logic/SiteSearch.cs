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
        Job job = new Job();
        HttpClient httpClient = new HttpClient();
        HttpResponseMessage site;
        HtmlNodeCollection preLinks;
        HtmlNodeCollection preTime;
        HtmlDocument doc = new HtmlDocument();
        IEnumerable<HtmlAgilityPack.HtmlNode> links;
        IEnumerable<HtmlAgilityPack.HtmlNode> time;
        string siteString;
        int counterInForeach = 0;

        public SiteSearch()
        {}

        public async Task<string> pph(List<Job> pphJobList)
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

                job.Title = node.GetAttributeValue("title", string.Empty);
                job.URL = node.GetAttributeValue("href", string.Empty);

                pphJobList.Add(job);
                System.Console.WriteLine("Added: {0}", job.URL);
            }

            foreach(var node in time){
                
                
                job.time = node.InnerText;

                pphJobList[counterInForeach].time = job.time;
                counterInForeach++;
            }
            counterInForeach = 0;

            for (int i = 0; i < pphJobList.Count; i++)
            {
                System.Console.WriteLine("HAP: Job: title: {0} \n URL: {1} \n time: {2} ",
                pphJobList[i].Title, pphJobList[i].URL, pphJobList[i].time);
            }
                

            System.Console.WriteLine("HAP: Finish");
            System.Console.WriteLine("Class SiteSearch: return: site");
            return siteString;
        }

    }
}