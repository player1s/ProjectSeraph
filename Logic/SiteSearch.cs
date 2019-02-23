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
        HtmlNodeCollection preProposalCount;
        HtmlDocument doc = new HtmlDocument();
        IEnumerable<HtmlAgilityPack.HtmlNode> links;
        IEnumerable<HtmlAgilityPack.HtmlNode> time;
        IEnumerable<HtmlAgilityPack.HtmlNode> proposals;
        string siteString;
        int foreachInteration = 0;
        List<Job> pphJobs = new List<Job>();
        List<string> times = new List<string>();
        List<string> proposalList = new List<string>();

        public SiteSearch()
        {}

        public async Task<List<Job>> pph()
        {

            System.Console.WriteLine("Class SiteSearch: Start");

            site = await httpClient.GetAsync("https://www.peopleperhour.com/freelance-jobs");
            siteString = await site.Content.ReadAsStringAsync();

            System.Console.WriteLine("HAP: Start");

            doc.LoadHtml(siteString);
            //Select nodes of different locations. Needed information are separated
            preLinks = doc.DocumentNode.SelectNodes("//div[contains(@class, 'main-content full-width')]//h6[contains(@class, 'title')]");
            preTime = doc.DocumentNode.SelectNodes("//div[contains(@class, 'main-content full-width')]//ul[contains(@class, 'clearfix member-info horizontal crop hidden-xs')]");
            preProposalCount = doc.DocumentNode.SelectNodes("//div[contains(@class, 'main-content full-width')]//span[contains(@class, 'value proposal-count')]");

            System.Console.WriteLine("HAP: precount {0}", preLinks.Count);
            //select tags on which specific queries will be run
            links = preLinks.Descendants("a");
            time = preTime.Descendants("time");
            proposals = preProposalCount.Nodes();

            //querying elements that are located in different nodes
            foreach(var node in proposals){

                proposalList.Add(node.InnerText);
                System.Console.WriteLine("proposallist added this: {0}", node.InnerText);
            }
            //querying elements that are located in different nodes
            foreach(var node in time){

                times.Add(node.InnerText);
            }
            // "Main" foreach where all the data are collected int a job object, then written to a List<Job>
            foreach(var node in links){
                
                Job job = new Job();

                job.Title = node.GetAttributeValue("title", string.Empty);
                job.URL = node.GetAttributeValue("href", string.Empty);
                job.Time = times[foreachInteration];
                job.ProposalNum = proposalList[foreachInteration];

                pphJobs.Add(job);
                foreachInteration++;
            }
            foreachInteration = 0;
            //check if the received data are adequate
           for (int i = 0; i < pphJobs.Count; i++)
           {
                System.Console.WriteLine("HAP: in List : title: {0} \n URL: {1}  Time: {2}  Proposals: {3}"
                ,pphJobs[i].Title, pphJobs[i].URL, pphJobs[i].Time, pphJobs[i].ProposalNum);
           }     

            System.Console.WriteLine("HAP: Finish");
            System.Console.WriteLine("Class SiteSearch: return: site");
            return pphJobs;
        }

    }
}