using CustomerDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebBff.API.Interfaces
{
    public interface ICustomerService
    {
        Task<IActionResult> DeleteAsync(Guid customerId, string accessToken);
        Task<CustomerView?> GetCustomerAsync(Guid customerId, string accessToken);
        Task<IEnumerable<CustomerView>?> GetCustomersAsync(string accessToken);
        Task<IActionResult> PostAsync(CustomerDto dto, string accessToken);
        Task<IActionResult> UpdateAsync(Guid customerId, CustomerDto dto, string accessToken);
    }
}
