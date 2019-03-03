using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Tier3.Model
{
    public class BloggingContext : DbContext
    {
        public DbSet<Job> Job { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;");
        }
    }
}