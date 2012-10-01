using BankTransactions.Controllers.Adapters;
using BankTransactions.Controllers.Util;
using nl.jbs.banktransactions;
using System.Collections.Generic;
using System.Linq;

namespace BankTransactions.Controllers
{
    public class RecordRetriever
    {
        public static IEnumerable<BankRecord> GetBankRecords()
        {
            return GetBankRecords(ConfigurationManager.BaseLocation + ConfigurationManager.FileName, ConfigurationManager.FileType).OrderByDescending(r => r.RequestDate);
        }

        public static IList<BankRecord> GetBankRecords(string file, TransactionAdapterType type)
        {
            TransactionAdapter adapter = TransactionAdapterFactory.GetAdapter(type);
            return adapter.ParseBankRecords(file);
        }
    }
}