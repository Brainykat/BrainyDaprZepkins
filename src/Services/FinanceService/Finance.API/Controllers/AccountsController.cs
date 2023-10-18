using Finance.Services.Interfaces;
using FinanceDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService service;

        public AccountsController(IAccountService AccountService)
        {
            this.service = AccountService ?? throw new ArgumentNullException(nameof(AccountService));
        }
        [HttpGet()]
        public async Task<IActionResult> GetAccounts() =>
      Ok(await service.GetAccounts());

        [HttpGet("{accountId:guid}")]
        public async Task<IActionResult> GetAccount(Guid accountId) =>
          Ok(await service.GetAccount(accountId));
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AccountDto dto)
        => await service.Add(dto, Guid.Empty);
        //=> await service.Add(dto, HttpContext.User.Claims.GetUserId());

        [HttpPut("{accountId:guid}")]
        public async Task<IActionResult> Update(Guid accountId, [FromBody] AccountDto dto)
        => await service.Update(accountId, dto);


        [HttpDelete("{accountId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid accountId)
        => await service.Delete(accountId);
    }
}
