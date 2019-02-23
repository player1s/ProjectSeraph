using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectSeraph.model;

namespace ProjectSeraph.Logic
{
    class Center
    {
        public Center()
        {}
        public async Task<string> core()
        {
            System.Console.WriteLine("Center: Start");

            String toReturn = "pph";

            SiteSearch siteSearch = new SiteSearch();

            List<Job> jobList = new List<Job>();

            await siteSearch.pph();
            //toReturn = siteSearch.responseString;
            System.Console.WriteLine("Center: Returns: pph");
            return toReturn;
        }

    }
}