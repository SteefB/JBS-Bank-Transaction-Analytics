﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Globalization;
using BankTransactions.Controllers.Util;
using Microsoft.VisualBasic.FileIO;
using nl.jbs.banktransactions;
using nl.jbs.banktransactions.Models;

namespace BankTransactions.Controllers.Adapters
{
    public class TransactionAdapterRabobank : TransactionAdapter
    {
        public void ParseBankRecords(string file)
        {
            List<BankRecord> records = new List<BankRecord>();

            TextFieldParser parser = new TextFieldParser(file);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");

            while (!parser.EndOfData)
            {
                try
                {
                    using (var db = new BankTransactionsContext())
                    {
                        records.Add(createRecord(parser.ReadFields()));
                    }
                }
                catch (ArgumentException e)
                {
                    // This record is either corrupt, or we've reached the end of the list: just continue on.
                    continue;
                }
            }
            // Save the records and other info into the database.
            using (var db = new BankTransactionsContext())
            {
                BankTransactionsUpload upload = new BankTransactionsUpload()
                {
                    fileName = file,
                    filePath = System.IO.Path.Combine(ConfigurationManager.BaseLocation, "Uploaded/" + DateTime.Now.Ticks + "_" + System.IO.Path.GetFileName(file)),
                    user = "test",
                    bankRecord = records
                };
                db.SaveChanges();
            }

            parser.Close();
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

            return new BankRecord()
            {
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