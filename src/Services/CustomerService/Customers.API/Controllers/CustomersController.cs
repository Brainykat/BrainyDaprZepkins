using CustomerDtos;
using Customers.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAuthenticate;
using Google.Api;

namespace Customers.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService service;

        public CustomersController(ICustomerService customerService)
        {
            this.service = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }
        [HttpGet()]
        public async Task<IActionResult> GetCustomers() =>
      Ok(await service.GetCustomers());

        [HttpGet("{customerId:guid}")]
        public async Task<IActionResult> GetCustomer(Guid customerId) =>
          Ok(await service.GetCustomer(customerId));
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CustomerDto dto)
        => await service.Add(dto, Guid.Empty);
        //=> await service.Add(dto, HttpContext.User.Claims.GetUserId());

        [HttpPut("{customerId:guid}")]
        public async Task<IActionResult> Update(Guid customerId, [FromBody] CustomerDto dto)
        => await service.Update(customerId, dto);


        [HttpDelete("{customerId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid customerId)
        => await service.Delete(customerId);
    }
}
