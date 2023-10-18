using Finance.Domain.Entities;
using Finance.Domain.Interfaces;
using Finance.Services.Interfaces;
using FinanceDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharedBase.ValueObjects;
using System.Net;
using System.Reflection;
using WebHosts;

namespace Finance.Services.Services
{
    public class AccountService : ControllerBase, IAccountService
    {
        private readonly IAccountRepository repo;
        //private readonly IRaiseBGAccountEvents raiseBGAccountEvents;
        private readonly ILogger<AccountService> logger;
        public AccountService(IAccountRepository repo, ILogger<AccountService> logger)
        {
            this.repo = repo ?? throw new ArgumentNullException(nameof(repo));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            //this.raiseBGAccountEvents = raiseBGAccountEvents ?? throw new ArgumentNullException(nameof(raiseBGAccountEvents));
        }
        public async Task<Account> GetAccount(Guid id) => await repo.GetAccount(id);
        public async Task<List<Account>> GetAccounts() => await repo.GetAccounts();
        public async Task<IActionResult> Add(AccountDto dto, Guid userId)
        {
            try
            {
                //UNDONE: Currency should not be hard coded
                var account = Account.Create(dto.CustomerId, dto.Name, dto.AccountNumber, dto.AccountBearerType,
                    Money.Create("KSh", dto.TransactionLimit));

                await repo.Add(account);
                //var user = new UserEBDto(Account.Name, Account.Email, Account.Phone, DateTime.Now.AddDays(7), Account.Id,
                //        new List<string> { "BGAccountAdmin" },
                //        new List<ClaimEBDto> {
                //    new ClaimEBDto { Type = "BGAccountId", Value = Account.Id.ToString() }
                //    //new ClaimEBDto { Type = "AgencyBranchId", Value = bb.Id.ToString() }
                //          }
                //       );

                //var two = raiseBGAccountEvents.RaiseBGAccountAdminCreation(user);
                //if (!two) logger.LogCritical($"Could not raise BGAccount Admin User Creation Event: {user.Serialize()}");
                return Ok(account);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(logger, ex, MethodBase.GetCurrentMethod());
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
        public async Task<IActionResult> Update(Guid AccountId, AccountDto dto)
        {
            try
            {
                var cat = await repo.GetAccountTracked(AccountId);
                if (cat == null) return NotFound();
                cat.Name = dto.Name;
                cat.AccountNumber = dto.AccountNumber;
                cat.AccountTransactionLimit = Money.Create("KSh", dto.TransactionLimit);
                await repo.Update(cat);
                return Ok(cat);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(logger, ex, MethodBase.GetCurrentMethod());
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
        public async Task<IActionResult> Delete(Guid AccountId)
        {
            try
            {
                var cat = await repo.GetAccountTracked(AccountId);
                if (cat == null) return NotFound();
                await repo.Delete(cat);
                return NoContent();
            }
            catch (Exception ex)
            {
                LogHelper.LogError(logger, ex, MethodBase.GetCurrentMethod());
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
