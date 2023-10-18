using Finance.Domain.Entities;
using Finance.Domain.Interfaces;

namespace Finance.Data.Repos
{
    

    public class LedgerRepository : ILedgerRepository
    {
        private readonly FinanceContext context;

        public LedgerRepository(FinanceContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }
        //   public async Task<List<LedgerEntryView>> GetPolicyEntries(Guid policyId) =>
        //  await context.LedgerEntries.AsNoTracking()
        //.Where(a => a.PolicyId == policyId)
        ////.OrderBy(a=>a.AccountId)
        //.ToListAsync();

        public async Task Add(Ledger debit, Ledger credit)
        {
            if (ValidateTxn(debit.Debit.Amount, credit.Credit.Amount))
            {
                context.Ledgers.Add(debit);
                context.Ledgers.Add(credit);
                await context.SaveChangesAsync();
            }
        }
        public async Task Add(List<Ledger> debits, Ledger credit)
        {
            if (ValidateTxn(debits.Sum(a => a.Debit.Amount), credit.Credit.Amount))
            {
                context.Ledgers.AddRange(debits);
                context.Ledgers.Add(credit);
                await context.SaveChangesAsync();
            }
        }
        public async Task Add(Ledger debit, List<Ledger> credits)
        {
            if (ValidateTxn(debit.Debit.Amount, credits.Sum(c => c.Credit.Amount)))
            {
                context.Ledgers.Add(debit);
                context.Ledgers.AddRange(credits);
                await context.SaveChangesAsync();
            }
        }
        public async Task Add(List<Ledger> debits, List<Ledger> credits)
        {
            if (ValidateTxn(debits.Sum(d => d.Debit.Amount), credits.Sum(c => c.Credit.Amount)))
            {
                context.Ledgers.AddRange(debits);
                context.Ledgers.AddRange(credits);
                await context.SaveChangesAsync();
            }
        }
        private bool ValidateTxn(decimal debit, decimal credit) => debit == credit;
    }
}
