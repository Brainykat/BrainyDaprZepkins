using Finance.Domain.Entities;

namespace Finance.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task Add(Account Account);
        Task Delete(Account Account);
        Task<Account> GetAccount(Guid id);
        Task<List<Account>> GetAccounts();
        Task<Account> GetAccountTracked(Guid id);
        Task Update(Account Account);
    }
}
