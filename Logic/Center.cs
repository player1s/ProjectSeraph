using System;


namespace ProjectSeraph.Logic
{
    class Center
    {
        public Center()
        {}
        public String core()
        {
            System.Console.WriteLine("Center: Start");

            String toReturn;

            SiteSearch siteSearch = new SiteSearch();
            toReturn = siteSearch.pph();

            System.Console.WriteLine("Center: Returns: {0}", toReturn);
            return toReturn;
        }

    }
}