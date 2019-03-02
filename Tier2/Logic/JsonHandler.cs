using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Tier2.model;

namespace Tier2.Logic
{
    class JsonHandler
    {
        String n = "";
        public JsonHandler(){}

        public String SerializeRange(List<Job> range)
        {

            n = JsonConvert.SerializeObject(range);

            System.Console.WriteLine("Serialized");

            return n;
        }
    }
}