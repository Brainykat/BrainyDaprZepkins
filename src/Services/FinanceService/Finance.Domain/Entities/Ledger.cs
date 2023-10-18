using SharedBase;
using SharedBase.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Domain.Entities
{
    public class Ledger :EntityBase
    {
        public static Ledger Create(Guid accountId, DateTime txnTime, Money credit, Money debit,
            Guid txnRefrence, string narration, Guid transactingUser)
            => new (accountId, txnTime, credit, debit, txnRefrence, narration, transactingUser);
        public Ledger(Guid accountId, DateTime txnTime, Money credit, Money debit,
            Guid txnRefrence, string narration, Guid transactingUser)
        {
            AccountId = accountId;
            TxnTime = txnTime;
            Credit = credit ?? throw new ArgumentNullException(nameof(credit));
            Debit = debit ?? throw new ArgumentNullException(nameof(debit));
            TxnRefrence = txnRefrence;
            Narration = narration ?? throw new ArgumentNullException(nameof(narration));
            TransactingUser = transactingUser;
        }
        private Ledger() { }
        public Guid AccountId { get; set; }
        public DateTime TxnTime { get; set; }
        public Money Credit { get; set; }
        public Money Debit { get; set; }
        public Guid TxnRefrence { get; set; }
        public string Narration { get; set; }
        public Guid TransactingUser { get; set; }
        public Account Account { get; set; }
    }
}
