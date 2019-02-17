using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjectSeraph.Logic
{
    class SiteSearch
    {
        public SiteSearch()
        {}
        public string pph()
        {
            System.Console.WriteLine("Class SiteSearch: Start");

            string toReturn = "pph";

            HttpClient client = new HttpClient();

            var site = client.GetAsync("https://www.peopleperhour.com/freelance-jobs");
            System.Console.WriteLine(site);

            System.Console.WriteLine("Class SiteSearch: return: {0}", toReturn);
            return toReturn;
        }

    }
}