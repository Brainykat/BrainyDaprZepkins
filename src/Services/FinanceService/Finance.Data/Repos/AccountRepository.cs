using Finance.Domain.Entities;
using Finance.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Finance.Data.Repos
{
    

    public class AccountRepository : IAccountRepository
    {
        private readonly FinanceContext context;

        public AccountRepository(FinanceContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            //context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<List<Account>> GetAccounts() =>
         await context.Accounts.AsNoTracking().ToListAsync();
        public async Task<Account> GetAccount(Guid id) =>
          await context.Accounts.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        public async Task<Account> GetAccountTracked(Guid id) =>
          await context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
        public async Task Add(Account Account)
        {
            context.Accounts.Add(Account);
            await context.SaveChangesAsync();
        }
        public async Task Update(Account Account)
        {
            context.Accounts.Update(Account);
            await context.SaveChangesAsync();
        }
        public async Task Delete(Account Account)
        {
            context.Accounts.Remove(Account);
            await context.SaveChangesAsync();
        }
    }
}
