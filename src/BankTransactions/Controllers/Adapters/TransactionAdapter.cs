using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nl.jbs.banktransactions;

namespace BankTransactions.Controllers.Adapters
{
    public interface TransactionAdapter
    {
        IList<BankRecord> ParseBankRecords(string file);
    }
}