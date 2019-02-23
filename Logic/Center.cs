using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectSeraph.model;

namespace ProjectSeraph.Logic
{
    class Center
    {
        List<Job> jobs = new List<Job>();
        SiteSearch siteSearch = new SiteSearch();
        string toReturn = "";

        public Center()
        {}
        public async Task<string> core()
        {
            System.Console.WriteLine("Center: Start");

            jobs.AddRange(await siteSearch.pph());

            for (int i = 0; i < jobs.Count; i++)
            {
                toReturn += "Title: " + jobs[i].Title + "URL: " + jobs[i].URL + "Time: " + jobs[i].Time + "Proposals: " + jobs[i].ProposalNum;
            }

            System.Console.WriteLine("Center: Returns: pph");

            return toReturn;
        }

    }
}