using System;
namespace Tier3.Logic
{
    public class JsonHandler
    {
        public JsonHandler(){}

         public List<Job> DeSerializeToRange(string range)
        {

            jobList = JsonConvert.DeserializeObject<List<Job>>(range);

            System.Console.WriteLine("Serialized");

            return jobList;
        }

        public String SerializeRange(List<Job> range)
        {

            n = JsonConvert.SerializeObject(range);

            System.Console.WriteLine("Serialized");

            return n;
        }
    }
}