using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Tier2.model;
using Newtonsoft.Json;

namespace Tier2.Logic
{
    class T2Client 
    {
        string responseInString;
        List<Job> deSerialized;
        string url;
        JsonHandler jsonHandler = new JsonHandler();
        public T2Client() 
        {}    
            public async Task<string> PostData(List<Job> obj)
        {
            string responseInString;
            string json = JsonConvert.SerializeObject(obj);
            string url = "https://localhost:5005/api/values/";
            HttpClient client = new HttpClient();
            var result = await client.PostAsJsonAsync(url, json).ConfigureAwait(false);

            responseInString = await result.Content.ReadAsStringAsync();

            return responseInString;
        }
    }
}
