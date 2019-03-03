using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Tier2.model;

namespace Tier2.Logic
{
    class T2Client 
    {
        string responseInString;
        List<Job> deSerialized;
        string url;
        //JsonHandler jsonHandler = new JsonHandler();
        public T2Client() 
        {}
        public async void GetData()
        {
            
            url = "https://localhost:5005/api/values/";
            HttpClient client = new HttpClient();
            var result = await client.GetAsync(url).ConfigureAwait(false);

            responseInString = await result.Content.ReadAsStringAsync();

            System.Console.WriteLine(responseInString);

            //deSerialized = jsonHandler.DeSerializeToRange(responseInString);

            //return deSerialized;
        }
    }
}