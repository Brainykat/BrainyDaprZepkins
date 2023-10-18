using Finance.Domain.Entities;

namespace Finance.Domain.Interfaces
{
    public interface ILedgerRepository
    {
        Task Add(Ledger debit, Ledger credit);
        Task Add(Ledger debit, List<Ledger> credits);
        Task Add(List<Ledger> debits, Ledger credit);
        Task Add(List<Ledger> debits, List<Ledger> credits);
    }
}
