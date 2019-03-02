using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RazorPagesMovie.Model;

namespace RazorPagesMovie.Logic
{
    class Client 
    {
        string responseInString;
        List<Job> deSerialized;
        string url;
        JsonHandler jsonHandler = new JsonHandler();
        public Client() 
        {}
        public async Task<List<Job>> GetData()
        {
            
            url = "https://localhost:5003/api/values/";
            HttpClient client = new HttpClient();
            var result = await client.GetAsync(url).ConfigureAwait(false);

            responseInString = await result.Content.ReadAsStringAsync();

            deSerialized = jsonHandler.DeSerializeToRange(responseInString);

            return deSerialized;
        }
    }
}