using System.Collections.Generic;
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using ProjectSeraph.model;
using System;

namespace ProjectSeraph.Logic
{
    class SiteSearch
    {
        HttpClient httpClient = new HttpClient();
        HttpResponseMessage site;
        HtmlNodeCollection preLinks;
        HtmlNodeCollection preTime;
        HtmlNodeCollection preProposalCount;
        HtmlNodeCollection prePriceTag;
        HtmlDocument doc = new HtmlDocument();
        IEnumerable<HtmlAgilityPack.HtmlNode> links;
        IEnumerable<HtmlAgilityPack.HtmlNode> time;
        IEnumerable<HtmlAgilityPack.HtmlNode> proposals;
        IEnumerable<HtmlAgilityPack.HtmlNode> price;
        IEnumerable<HtmlAgilityPack.HtmlNode> isFixedSalary;
        string siteString;
        int foreachInteration = 0;
        List<Job> pphJobs = new List<Job>();
        List<DateTime> timeList = new List<DateTime>();
        string[] proposalList = new string[120];
        List<string> priceList = new List<string>();
        List<string> isFixedSalaryList = new List<string>();
        // filtertime set to half hrs
        System.TimeSpan filterTime = new System.TimeSpan(0, 0, -30, 0);
        
        public SiteSearch()
        {}

        //Query website peopleperhour
        public async Task<List<Job>> pph()
        {

            System.Console.WriteLine("Class SiteSearch: Start");

            // the site to check on
            site = await httpClient.GetAsync("https://www.peopleperhour.com/freelance-jobs");
            siteString = await site.Content.ReadAsStringAsync();

            System.Console.WriteLine("HAP: Start");

            doc.LoadHtml(siteString);
            //Select nodes of different locations. Needed information are separated
            preLinks = doc.DocumentNode.SelectNodes("//div[contains(@class, 'main-content full-width')]//h6[contains(@class, 'title')]");
            preTime = doc.DocumentNode.SelectNodes("//div[contains(@class, 'main-content full-width')]//ul[contains(@class, 'clearfix member-info horizontal crop hidden-xs')]");
            preProposalCount = doc.DocumentNode.SelectNodes("//div[contains(@class, 'main-content full-width')]//span[contains(@class, 'value proposal-count')]");
            prePriceTag = doc.DocumentNode.SelectNodes("//div[contains(@class, 'main-content full-width')]//div[contains(@class, 'price-tag')]");

            System.Console.WriteLine("HAP: precount {0}", preLinks.Count);
            //select tags on which specific queries will be run
            links = preLinks.Descendants("a");
            time = preTime.Descendants("time");
            proposals = preProposalCount.Nodes();
            price = prePriceTag.Descendants("span");
            isFixedSalary = prePriceTag.Descendants("small");

            foreach(var node in isFixedSalary){

                isFixedSalaryList.Add(node.InnerText);
                System.Console.WriteLine("isFixedSalaryList added this: {0}", node.InnerText);
            }

            //querying elements that are located in different nodes
            foreach(var node in price){

                priceList.Add(node.InnerText);
                System.Console.WriteLine("priceList added this: {0}", node.InnerText);
            }

            //querying elements that are located in different nodes
            foreach(var node in proposals){

                proposalList[foreachInteration] = node.InnerText;
                System.Console.WriteLine("proposallist added this: {0}", node.InnerText);
                foreachInteration++;
            }
            // reset foreachIteration for later use
            foreachInteration = 0;

            //querying elements that are located in different nodes
            foreach(var node in time){

                DateTime timePosted = Convert.ToDateTime(node.GetAttributeValue("datetime", string.Empty));

                timeList.Add(timePosted);
            }
            // "Main" foreach where all the data are collected into a job object, then written to a List<Job>
            foreach(var node in links){
                
                Job job = new Job();
                System.Console.WriteLine("foreach: {0}", foreachInteration);
                job.Title = node.GetAttributeValue("title", string.Empty);
                job.URL = node.GetAttributeValue("href", string.Empty);
                job.Time = timeList[foreachInteration];
                job.ProposalNum = proposalList[foreachInteration];
                job.Salary = priceList[foreachInteration];
                job.isFixedSalary = isFixedSalaryList[foreachInteration];

                //Check that the jobs were posted within a specified timeframe from now.
                if(job.Time > DateTime.Now.Add(filterTime))
                {
                pphJobs.Add(job);
                }
                foreachInteration++;
            }
            foreachInteration = 0;

            System.Console.WriteLine("HAP: Finish");
            System.Console.WriteLine("Class SiteSearch: return: site");
            return pphJobs;
        }

    }
}