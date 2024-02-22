using CustomerDtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebBff.API.Interfaces;

namespace WebBff.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService service;
        public CustomersController(ICustomerService customerService)
        {
            service = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }
        [HttpGet()]
        [ProducesResponseType(typeof(List<CustomerView>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get([FromHeader] string authorization) =>
      Ok(await service.GetCustomersAsync(authorization));

        [HttpGet("{customerId:guid}")]
        [ProducesResponseType(typeof(CustomerView), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get(Guid customerId, [FromHeader] string authorization) =>
      Ok(await service.GetCustomerAsync(customerId, authorization));

        [HttpPost]
        //[HttpPut]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomerView), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post(
        [FromBody] CustomerDto dto,
        [FromHeader] string authorization)
        {
            return await service.PostAsync(dto, authorization);
        }

        [HttpPut("{customerId:guid}")]
        [ProducesResponseType(typeof(CustomerView), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomerView), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Put(Guid customerId,
        [FromBody] CustomerDto dto,
        [FromHeader] string authorization)
        {
            return await service.UpdateAsync(customerId, dto, authorization);
        }

        [HttpDelete("{customerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid customerId, [FromHeader] string authorization) =>
            await service.DeleteAsync(customerId,authorization);
    }
}
