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
        public string core()
        {
            System.Console.WriteLine("Center: Start");

            jobs.AddRange(siteSearch.pph().Result);

            //prepare the whole array into a string to return.
           for (int i = 0; i < jobs.Count; i++)
            {
                toReturn += "\n Title: " + jobs[i].Title + "\n URL: " + jobs[i].URL + "\n Time: "
                 + jobs[i].Time + "\n Proposals: " + jobs[i].ProposalNum + "\n Price: " + jobs[i].Salary + "\n";
            } 

            System.Console.WriteLine("Center: Returns: {0}", toReturn);

            return toReturn;
        }

    }
}