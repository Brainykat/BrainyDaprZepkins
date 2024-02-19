using CustomerDtos;
using Customers.Domain.Entities;
using Customers.Domain.Interfaces;
using Customers.Services.Interfaces;
using EventBus;
using EventBus.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharedBase.ValueObjects;
using System.Net;
using System.Reflection;
using WebHosts;

namespace Customers.Services.Services
{
    public class CustomerService : ControllerBase, ICustomerService
    {
        private readonly ICustomerRepository repo;
        private readonly IEventBus _eventBus;
        private readonly ILogger<CustomerService> logger;
        public CustomerService(ICustomerRepository repo, ILogger<CustomerService> logger
, IEventBus eventBus
            )
        {
            this.repo = repo ?? throw new ArgumentNullException(nameof(repo));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventBus = eventBus;
            //this.raiseBGCustomerEvents = raiseBGCustomerEvents ?? throw new ArgumentNullException(nameof(raiseBGCustomerEvents));
        }
        public async Task<Customer> GetCustomer(Guid id) => await repo.GetCustomer(id);
        public async Task<List<Customer>> GetCustomers() => await repo.GetCustomers();
        public async Task<IActionResult> Add(CustomerDto dto, Guid userId)
        {
            try
            {
                var customer = Customer.Create(Name.Create(dto.SurName, dto.FirstName, dto.MiddleName), dto.Phone, dto.Email);

                await repo.Add(customer);
                var eventMessage = new CustomerEBDto(customer.Id, customer.Name.FullName );

                await _eventBus.PublishAsync(eventMessage);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(logger, ex, MethodBase.GetCurrentMethod());
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
        public async Task<IActionResult> Update(Guid CustomerId, CustomerDto dto)
        {
            try
            {
                var cat = await repo.GetCustomerTracked(CustomerId);
                if (cat == null) return NotFound();
                cat.Name = Name.Create(dto.SurName, dto.FirstName, dto.MiddleName);
                cat.Phone = dto.Phone;
                cat.Email = dto.Email;
                await repo.Update(cat);
                return Ok(cat);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(logger, ex, MethodBase.GetCurrentMethod());
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
        public async Task<IActionResult> Delete(Guid CustomerId)
        {
            try
            {
                //throw new Exception("This is a test exception");
                var cat = await repo.GetCustomerTracked(CustomerId);
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
