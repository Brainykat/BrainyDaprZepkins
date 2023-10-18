using SharedBase;
using SharedBase.Enums;
using SharedBase.ValueObjects;

namespace Finance.Domain.Entities
{
    public class Account: EntityBase
    {
        public static Account Create(Guid customerId, string name, string accountNumber,
            AccountBearerType accountBearerType, Money accountTransactionLimit)
            => new(customerId, name, accountNumber, accountBearerType, accountTransactionLimit);
        public Account(Guid customerId, string name, string accountNumber,
            AccountBearerType accountBearerType, Money accountTransactionLimit)
        {
            CustomerId = customerId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            AccountNumber = accountNumber ?? throw new ArgumentNullException(nameof(accountNumber));
            AccountBearerType = accountBearerType;
            AccountTransactionLimit = accountTransactionLimit ?? throw new ArgumentNullException(nameof(accountTransactionLimit));
        }
        private Account() { }
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public AccountBearerType AccountBearerType { get; set; }
        //public Guid ChartOfAccountId { get; set; }
        public Money AccountTransactionLimit { get; set; }
        public IEnumerable<Ledger> Ledgers { get; set; }
    }
}
