
using System;

namespace ProjectSeraph.model
{
    class Job
    {
        public Job()
        {}
        public string Title { get; set; }
        public string URL { get; set; }
        public DateTime Time { get; set; }
        public string Salary { get; set; }
        public string ProposalNum { get; set; }
        public string isFixedSalary { get; set; }

    }
}