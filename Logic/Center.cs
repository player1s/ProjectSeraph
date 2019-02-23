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

        public Center()
        {}
        public async Task<List<Job>> core()
        {
            System.Console.WriteLine("Center: Start");

            jobs.AddRange(await siteSearch.pph());

            System.Console.WriteLine("Center: Returns: pph");

            return jobs;
        }

    }
}