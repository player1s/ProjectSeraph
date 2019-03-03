using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Tier3.Model
{
    public class EFBase : DbContext
    {
        public DbSet<Job> Jobs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Jobs.db");
        }
    }
}