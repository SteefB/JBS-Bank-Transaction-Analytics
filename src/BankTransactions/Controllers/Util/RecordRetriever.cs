using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nl.jbs.banktransactions;
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