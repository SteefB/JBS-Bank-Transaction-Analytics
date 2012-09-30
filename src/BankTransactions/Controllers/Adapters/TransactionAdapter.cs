using nl.jbs.banktransactions;
using System.Collections.Generic;

namespace BankTransactions.Controllers.Adapters
{
    public interface TransactionAdapter
    {
        IList<BankRecord> ParseBankRecords(string file);
    }
}