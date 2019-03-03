using System;
namespace Tier3.Logic
{
    public class Core
    {
        JsonHandler jsonHandler = new JsonHandler();
        List<Job> jobList = new List<Job>();
        public Core(){}

        public void Base(string json)
        {
            jobList = jsonHandler.DeSerializeToRange(json);

            using (var db = new JobContext())
            {
                db.Job.Add(new Job { Url = "http://blogs.msdn.com/adonet" });
                var count = db.SaveChanges();
                Console.WriteLine("{0} records saved to database", count);

                Console.WriteLine();
                Console.WriteLine("All blogs in database:");
                foreach (var blog in db.Blogs)
                {
                    Console.WriteLine(" - {0}", blog.Url);
                }
            }
        }
    }
}