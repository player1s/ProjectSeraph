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
        T2Client t2Client = new T2Client();
        string toReturn = "";

        public Center()
        {}
        public String core()
        {
            System.Console.WriteLine("Center: Start");

            //jobs.AddRange(siteSearch.pph().Result);
            jobs.AddRange(siteSearch.workana().Result);
            
            toReturn = jsonHandler.SerializeRange(jobs);

            // send it to tier 3
            //t2Client.PostData(toReturn);

            System.Console.WriteLine("Center: Returns: {0}", toReturn);

            return toReturn;
        }

    }
}