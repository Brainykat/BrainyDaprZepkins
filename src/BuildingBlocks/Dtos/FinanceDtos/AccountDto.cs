using SharedBase.Enums;

namespace FinanceDtos
{
    public class AccountDto
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public AccountBearerType AccountBearerType { get; set; }
        public decimal TransactionLimit { get; set; }
    }
}