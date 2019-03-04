using System;
using Tier3.Model;
using Tier3.Logic;
using System.Collections.Generic;

namespace Tier3
{
    public class Program
    {
        public static void Main()
        {
            string json = "";
            List<Job> jobList = new List<Job>();
            DataHandler dataHandler = new DataHandler();
            JsonHandler jsonHandler = new JsonHandler();
            Receiver rec = new Receiver();

            json = rec.StartServer();
            jobList = jsonHandler.DeSerializeToRange(json);
            dataHandler.WriteData(jobList);            

            dataHandler.peek();
        }
    }
}
