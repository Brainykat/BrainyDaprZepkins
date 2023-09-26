using SharedBase;
using SharedBase.ValueObjects;

namespace Customers.Domain.Entities
{
    public class Customer : EntityBase
    {
        public static Customer Create(Name name, string phone, string email)
            => new Customer(name, phone, email);
        public Customer(Name name, string phone, string email)
        {
            GenerateNewIdentity();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Phone = phone ?? throw new ArgumentNullException(nameof(phone));
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }
        private Customer() { }
        public Name Name { get;  set; }
        public string Phone { get;  set; }
        public string Email { get;  set; }
    }
}
