﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using BankTransactions.Controllers.Util;
using Microsoft.VisualBasic.FileIO;
using nl.jbs.banktransactions;
using nl.jbs.banktransactions.Models;

namespace BankTransactions.Controllers.Adapters
{
    public class TransactionAdapterING : TransactionAdapter
    {
        public void ParseBankRecords(string file)
        {
            List<BankRecord> records = new List<BankRecord>();

            TextFieldParser parser = new TextFieldParser(file);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            parser.ReadFields();

            while (!parser.EndOfData)
            {
                try
                {
                    string[] fields = parser.ReadFields();

                    string mutationType = fields[4];
                    string nameDefinition = String.Empty;
                    string announcement = String.Empty;
                    string[] parts;

                    switch (mutationType)
                    {
                        case "BA":
                            parts = fields[8].Split(new string[] { " / " }, StringSplitOptions.None);
                            nameDefinition = parts.First();
                            announcement = parts.Last() + fields[1];
                            break;

                        case "GT":
                        case "OV":
                        default:
                            nameDefinition = fields[1];
                            announcement = fields[8];
                            break;

                        case "VZ":
                        case "IC":
                        case "DV":
                            nameDefinition = fields[8];
                            announcement = fields[1];
                            break;

                        case "GM":

                            // If there exist angle brackets in the second field, return the first two parts divided by those brackets
                            if (fields[1].Contains('>'))
                            {
                                parts = fields[1].Split(new string[] { ">" }, StringSplitOptions.None);
                                nameDefinition = String.Join(" ", new string[] { parts[0], parts[1] });
                                announcement = fields[1].Split(new string[] { ">" }, StringSplitOptions.None).Last() + fields[8];
                            }
                            else
                            {
                                parts = fields[8].Split(new string[] { " / " }, StringSplitOptions.None);
                                nameDefinition = parts.First();
                                announcement = parts.Last() + fields[1];
                            }
                            break;
                    }

                    BankRecord record = new BankRecord()
                    {
                        ExecutionDate = DateTime.Parse(fields[0], CultureInfo.CurrentCulture),
                        RequestDate = DateTime.Parse(fields[0], CultureInfo.CurrentCulture),
                        Name = nameDefinition,
                        BankNum = fields[2],
                        OtherBankNumber = fields[3],
                        Type = fields[5] == "Bij" ? BankRecord.TransactionType.CREDIT : BankRecord.TransactionType.DEBIT,
                        Amount = Double.Parse(fields[6]),
                        Description1 = announcement
                    };

                    records.Add(record);
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
    }
}