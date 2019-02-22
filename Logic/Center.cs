using System;
using System.Threading.Tasks;

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
            await siteSearch.pph();
            //toReturn = siteSearch.responseString;
            System.Console.WriteLine("Center: Returns: pph");
            return toReturn;
        }

    }
}