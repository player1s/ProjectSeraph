using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Tier3.Model;
using System;

namespace Tier3.Logic
{
    public class DataHandler 
    {
        List<Job> jobList = new List<Job>();   

        public DataHandler(){}        

        public List<Job> GetAllJobs()
        {
            using (var db = new EFBase())
            {
                foreach (var melo in db.Jobs)
                {
                    Job TempJob = new Job();
                    TempJob.Id = melo.Id;
                    TempJob.Title = melo.Title;
                    TempJob.URL = melo.URL;
                    TempJob.ProposalNum = melo.ProposalNum;
                    TempJob.Salary = melo.Salary;
                    TempJob.Time = melo.Time;
                    TempJob.isFixedSalary = melo.isFixedSalary;
                    Console.WriteLine(" - {0}", melo.Id);
                }
            }
            return jobList;            
        }

        public string WriteData(List<Job> range)
        {
             using (var db = new EFBase())
            {
                for (int i = 0; i < range.Count; i++)
                {
                    Job TempJob = new Job();
                    
                    TempJob.Id = range[i].Id;
                    TempJob.Title = range[i].Title;
                    TempJob.URL = range[i].URL;
                    TempJob.ProposalNum = range[i].ProposalNum;
                    TempJob.Salary = range[i].Salary;
                    TempJob.Time = range[i].Time;
                    TempJob.isFixedSalary = range[i].isFixedSalary;

                    db.Jobs.Add(TempJob);
                }
                var count = db.SaveChanges();
                Console.WriteLine("{0} records saved to database", count);

                Console.WriteLine();
                Console.WriteLine("All Jobs in database:");
                foreach (var melo in db.Jobs)
                {
                    Console.WriteLine(" - {0}", melo.Id);
                }
            }

            return "Added to db: ";
        }
    }
}