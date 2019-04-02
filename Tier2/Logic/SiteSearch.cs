using System.Collections.Generic;
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Tier2.model;
using System;

namespace Tier2.Logic
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
        string defaultValue = "-";
        int foreachInteration = 0;
        List<Job> jobsToReturn = new List<Job>();
        List<DateTime> timeList = new List<DateTime>();
        string[] proposalList = new string[120];
        List<string> priceList = new List<string>();
        List<string> isFixedSalaryList = new List<string>();
        // filtertime set to half hrs
        System.TimeSpan filterTime = new System.TimeSpan(-20, 0, -30, 0);
        //correction of timezones
        System.TimeSpan workanaTimezoneCorrection = new System.TimeSpan(0, 5, 0, 0);

        
        public SiteSearch()
        {}

        //Query website peopleperhour
        public async Task<List<Job>> pph()
        {
            //clear arrays /lists before use
            jobsToReturn.Clear();
            timeList.Clear();
            priceList.Clear();
            priceList.Clear();

            System.Console.WriteLine("Class SiteSearch: Start");

            //phase 1: connect to site
            // the site to check on
            site = await httpClient.GetAsync("https://www.peopleperhour.com/freelance-jobs");

            //phase 2: load the site
            siteString = await site.Content.ReadAsStringAsync();

            System.Console.WriteLine("HAP: Start");

            doc.LoadHtml(siteString);

            //phase 3: look for specific nodes
            //Select nodes of different locations. Needed information are separated
            preLinks = doc.DocumentNode.SelectNodes("//div[contains(@class, 'main-content full-width')]//h6[contains(@class, 'title')]");
            preTime = doc.DocumentNode.SelectNodes("//div[contains(@class, 'main-content full-width')]//ul[contains(@class, 'clearfix member-info horizontal crop hidden-xs')]");
            preProposalCount = doc.DocumentNode.SelectNodes("//div[contains(@class, 'main-content full-width')]//span[contains(@class, 'value proposal-count')]");
            prePriceTag = doc.DocumentNode.SelectNodes("//div[contains(@class, 'main-content full-width')]//div[contains(@class, 'price-tag')]");

            System.Console.WriteLine("HAP: precount {0}", preLinks.Count);

            //phase 4: select all or specific elements in nodes
            //select tags on which specific queries will be run
            links = preLinks.Descendants("a");
            time = preTime.Descendants("time");
            proposals = preProposalCount.Nodes();
            price = prePriceTag.Descendants("span");
            isFixedSalary = prePriceTag.Descendants("small");

            //phase 5: add selected elements to a list
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

            //phase 6: unify the collected elements in one object
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
                jobsToReturn.Add(job);
                }
                foreachInteration++;
            }
            foreachInteration = 0;

            System.Console.WriteLine("HAP: Finish");
            System.Console.WriteLine("Class SiteSearch: return: site");
            return jobsToReturn;
        }

        //------------------------------------------------------ Workana query ------------------

        public async Task<List<Job>> workana()
        {
            jobsToReturn.Clear();
            timeList.Clear();

            System.Console.WriteLine("Class SiteSearch: Start");

            //phase 1: connect to site
            // the site to check on
            site = await httpClient.GetAsync("https://www.workana.com/en/jobs?category=it-programming");

            //phase 2: load the site
            siteString = await site.Content.ReadAsStringAsync();

            System.Console.WriteLine("HAP: Start");

            doc.LoadHtml(siteString);

            //phase 3: look for specific nodes
            //Select nodes of different locations. Needed information are separated
            preLinks = doc.DocumentNode.SelectNodes("//div[contains(@class, 'col-sm-12 col-md-8 search-results')]//h2[contains(@class, 'h2 project-title')]");
            preTime = doc.DocumentNode.SelectNodes("//div[contains(@class, 'col-sm-12 col-md-8 search-results')]//div[contains(@class, 'project-header')]");
            preProposalCount = doc.DocumentNode.SelectNodes("//div[contains(@class, 'col-sm-12 col-md-8 search-results')]//span[contains(@class, 'bids')]");

            System.Console.WriteLine("HAP: precount {0}", preLinks.Count);

            //phase 4: select all or specific elements in nodes
            //select tags on which specific queries will be run
            links = preLinks.Descendants("a");
            time = preTime.Descendants("h5");
            proposals = preProposalCount.Nodes();

            //phase 5: add selected elements to a list
            //querying elements that are located in different nodes
            foreach(var node in proposals){

                if(int.TryParse(node.InnerText, out int n))
                proposalList[foreachInteration] = node.InnerText;
               // System.Console.WriteLine("proposallist passed on this: {0}", node.InnerText);
            }
            foreachInteration++;
            // reset foreachIteration for later use
            foreachInteration = 0;

            //querying elements that are located in different nodes
            foreach(var node in time){

                //System.Console.WriteLine(foreachInteration);
                DateTime timePosted = Convert.ToDateTime(node.GetAttributeValue("title", "01/01/2000 01.01.01"));

                System.Console.WriteLine("time added: " + timePosted);

                timeList.Add(timePosted);
                foreachInteration++;
            }
            // reset foreachIteration for later use
            foreachInteration = 0;

            //phase 6: unify the collected elements in one object
            // "Main" foreach where all the data are collected into a job object, then written to a List<Job>
            foreach(var node in links){
                
                Job job = new Job();

                job.Title = defaultValue;
                job.URL = defaultValue;
                job.ProposalNum = defaultValue;
                job.Salary = defaultValue;
                job.isFixedSalary = defaultValue;

                //System.Console.WriteLine("foreach: {0}", foreachInteration);
                job.Title = node.InnerText;
                job.URL = "https://www.workana.com" + node.GetAttributeValue("href", string.Empty);
                job.Time = timeList[foreachInteration].Add(workanaTimezoneCorrection);
                job.ProposalNum = proposalList[foreachInteration];

                System.Console.WriteLine("time added: " + job.Time + " with title: " + job.Title);
                
                //Check that the jobs were posted within a specified timeframe from now.
                if(job.Time > DateTime.Now.Add(filterTime))
                {
                jobsToReturn.Add(job);
                }
                foreachInteration++;
            }
            foreachInteration = 0;

            System.Console.WriteLine("HAP: Finish");
            System.Console.WriteLine("Class SiteSearch: return: site");
            return jobsToReturn;
        }

    }
}