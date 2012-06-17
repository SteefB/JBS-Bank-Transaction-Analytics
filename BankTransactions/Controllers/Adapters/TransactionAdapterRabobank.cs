using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nl.jorncruijsen.jbs.transactions;
using Microsoft.VisualBasic.FileIO;
using System.Collections.ObjectModel;

namespace BankTransactions.Controllers.Adapters
{
    public class TransactionAdapterRabobank : TransactionAdapter
    {
        public IList<BankRecord> ParseBankRecords(string file)
        {
            IList<BankRecord> records = new Collection<BankRecord>();

            TextFieldParser parser = new TextFieldParser(file);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            while (!parser.EndOfData)
            {
                try
                {
                    records.Add(new BankRecord(parser.ReadFields()));
                }
                catch (ArgumentException e)
                {
                    // This record is either corrupt, or we've reached the end of the list: just continue on.
                    continue;
                }
            }

            parser.Close();

            return records;
        }
    }
}