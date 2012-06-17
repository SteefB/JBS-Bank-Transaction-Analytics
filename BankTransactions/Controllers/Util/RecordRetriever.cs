using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nl.jorncruijsen.jbs.transactions;
using Microsoft.VisualBasic.FileIO;
using System.Collections.ObjectModel;
using BankTransactions.Controllers.Adapters;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using BankTransactions.Controllers.Util;

namespace BankTransactions.Controllers
{
    public class RecordRetriever
    {
        // TODO: Get these resources globally configurable
        private static string fileNameKey = "resourceFileName";
        private static string fileTypeKey = "resourceFileType";

        public static IEnumerable<BankRecord> GetBankRecords()
        {
            return GetBankRecords(ConfigurationManager.BaseLocation + "ing_alex.csv", TransactionAdapterType.ING).OrderBy(r => r.ExecutionDate);
        }

        public static IList<BankRecord> GetBankRecords(string file, TransactionAdapterType type)
        {
            TransactionAdapter adapter = TransactionAdapterFactory.GetAdapter(type);
            return adapter.ParseBankRecords(file);
        }
    }
}