using Customers.Domain.Entities;
using Customers.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Customers.Data.Repos
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomersContext context;

        public CustomerRepository(CustomersContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<Customer>> GetCustomers() =>
         await context.Customers.AsNoTracking().ToListAsync();
        public async Task<Customer> GetCustomer(Guid id) =>
          await context.Customers.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        public async Task<Customer> GetCustomerTracked(Guid id) =>
          await context.Customers.FirstOrDefaultAsync(a => a.Id == id);
        public async Task Add(Customer customer)
        {
            context.Customers.Add(customer);
            await context.SaveChangesAsync();
        }
        public async Task Update(Customer customer)
        {
            context.Customers.Update(customer);
            await context.SaveChangesAsync();
        }
        public async Task Delete(Customer customer)
        {
            context.Customers.Remove(customer);
            await context.SaveChangesAsync();
        }
    }
}
