using System.Collections.Generic;
using nl.jbs.banktransactions;

namespace BankTransactions.Controllers.Adapters
{
    public interface TransactionAdapter
    {
        void ParseBankRecords(string file);
    }
}