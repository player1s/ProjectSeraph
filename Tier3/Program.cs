using System;
using Tier3.Model;
using Tier3.Logic;

namespace Tier3
{
    public class Program
    {
        public static void Main()
        {
            DataHandler dataHandler = new DataHandler();
            Receiver rec = new Receiver();
            rec.StartServer();

            System.Console.WriteLine( dataHandler.GetAllJobs());
        }
    }
}
