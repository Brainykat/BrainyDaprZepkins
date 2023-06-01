using CustomerDtos;
using Customers.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IActionResult> Add(CustomerDto dto, Guid userId);
        Task<IActionResult> Delete(Guid CustomerId);
        Task<Customer> GetCustomer(Guid id);
        Task<List<Customer>> GetCustomers();
        Task<IActionResult> Update(Guid CustomerId, CustomerDto dto);
    }
}
