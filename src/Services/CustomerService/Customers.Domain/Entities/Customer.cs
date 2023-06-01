using SharedBase;
using SharedBase.ValueObjects;

namespace Customers.Domain.Entities
{
    public class Customer : EntityBase
    {
        public Name Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
