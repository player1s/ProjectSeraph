using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Tier3.Model;

namespace Tier3.Logic
{
    class JsonHandler
    {
        String n = "";
        List<Job> jobList = new List<Job>();
        public JsonHandler(){}

        public String SerializeRange(List<Job> range)
        {

            n = JsonConvert.SerializeObject(range);

            System.Console.WriteLine("Serialized");

            return n;
        }

        public List<Job> DeSerializeToRange(string range)
        {

            jobList = JsonConvert.DeserializeObject<List<Job>>(range);

            System.Console.WriteLine("Serialized");

            return jobList;
        }
    }
}