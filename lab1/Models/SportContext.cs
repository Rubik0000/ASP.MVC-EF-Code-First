using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace lab1.Models
{
    public class SportContext : DbContext
    {
        public SportContext() : base("SportContext") { }

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Contract> Contracts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}