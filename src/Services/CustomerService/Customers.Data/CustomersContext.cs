using Customers.Data.Configs;
using Customers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Customers.Data
{
    public class CustomersContext : DbContext
    {
        public CustomersContext(DbContextOptions<CustomersContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<LedgerEntryView>(d =>
            //{
            //  d.HasNoKey();
            //  d.ToView("View_Ledger_Entries");
            //});
            modelBuilder.ApplyConfiguration(new CustomerConfigs());
            
            //InitialAccountsSeed.InitialSeed(modelBuilder);
        }
    }
}
