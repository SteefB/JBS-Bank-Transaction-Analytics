using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nl.jbs.banktransactions;
using Microsoft.VisualBasic.FileIO;
using System.Collections.ObjectModel;
using System.Globalization;

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
                    records.Add(createRecord(parser.ReadFields()));
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

        public BankRecord createRecord(string[] args)
        {
            if (args.Length != 16)
            {
                throw new ArgumentException("Corrupt record, please fix.");
            }

            // Ninth value in the args is some kind of abbreviation for some magical thing
            BankRecord.TransactionCategory cat;
            Enum.TryParse<BankRecord.TransactionCategory>(args[8].ToUpper(), out cat);

            return new BankRecord() {
                // First value is the bank number
                BankNum = args[0],

                // Second value is the currency
                Currency = args[1],

                // Third value is the request date
                RequestDate = parseDate(args[2]),

                // Fourth value is the type of transaction
                Type = args[3] == "C" ? BankRecord.TransactionType.CREDIT : BankRecord.TransactionType.DEBIT,

                // Fifth value is the transferred amount
                Amount = Double.Parse(args[4], new CultureInfo("en-US")),

                // Sixth value is the other bank account number (or zeroes if there is none)
                OtherBankNumber = args[5],

                // Seventh value is the other bank account name
                Name = args[6].Replace('&', '|'),

                // Eigth value is the execution date
                ExecutionDate = parseDate(args[7]),
                Category = cat,

                // Tenth value appears to always be empty..
                Unknown = args[9],

                // Eleven to sixteen is the description (which could include transaction identifiers and/or exchange rates)
                Description1 = args[10],
                Description2 = args[11],
                Description3 = args[12],
                Description4 = args[13],
                Description5 = args[14],
                Description6 = args[15]
            };
        }

        private DateTime parseDate(string date)
        {
            if (date.Length != 8)
            {
                throw new ArgumentException("Corrupt date string: " + date);
            }

            return new DateTime(Int16.Parse(date.Substring(0, 4)), Int16.Parse(date.Substring(4, 2)), Int16.Parse(date.Substring(6, 2)));
        }
    }
}