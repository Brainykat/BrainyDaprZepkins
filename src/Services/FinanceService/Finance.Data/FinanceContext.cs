using Finance.Data.Configs;
using Finance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Data
{
    public class FinanceContext : DbContext
    {
        public FinanceContext(DbContextOptions<FinanceContext> options) : base(options)
        {
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Ledger> Ledgers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<LedgerEntryView>(d =>
            //{
            //  d.HasNoKey();
            //  d.ToView("View_Ledger_Entries");
            //});
            modelBuilder.ApplyConfiguration(new AccountConfigs());
            modelBuilder.ApplyConfiguration(new LedgerConfigs());

            //InitialAccountsSeed.InitialSeed(modelBuilder);
        }
    }
}
