using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nl.jorncruijsen.jbs.transactions;

namespace BankTransactions.Controllers.Adapters
{
    public interface TransactionAdapter
    {
        IList<BankRecord> ParseBankRecords(string file);
    }
}