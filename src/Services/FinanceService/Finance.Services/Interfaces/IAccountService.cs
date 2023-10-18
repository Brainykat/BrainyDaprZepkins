using Finance.Domain.Entities;
using FinanceDtos;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IActionResult> Add(AccountDto dto, Guid userId);
        Task<IActionResult> Delete(Guid AccountId);
        Task<Account> GetAccount(Guid id);
        Task<List<Account>> GetAccounts();
        Task<IActionResult> Update(Guid AccountId, AccountDto dto);
    }
}
