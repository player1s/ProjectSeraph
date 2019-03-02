using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tier2.model;

namespace Tier2.Logic
{
    class Center
    {
        public List<Job> jobs = new List<Job>();
        SiteSearch siteSearch = new SiteSearch();
        JsonHandler jsonHandler = new JsonHandler();
        string toReturn = "";

        public Center()
        {}
        public String core()
        {
            System.Console.WriteLine("Center: Start");

            jobs.AddRange(siteSearch.pph().Result);

            toReturn = jsonHandler.SerializeRange(jobs);

            System.Console.WriteLine("Center: Returns: {0}", toReturn);

            return toReturn;
        }

    }
}