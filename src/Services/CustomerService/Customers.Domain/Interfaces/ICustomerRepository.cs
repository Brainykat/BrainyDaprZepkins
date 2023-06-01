using Customers.Domain.Entities;

namespace Customers.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task Add(Customer customer);
        Task Delete(Customer customer);
        Task<Customer> GetCustomer(Guid id);
        Task<List<Customer>> GetCustomers();
        Task<Customer> GetCustomerTracked(Guid id);
        Task Update(Customer customer);
    }
}
