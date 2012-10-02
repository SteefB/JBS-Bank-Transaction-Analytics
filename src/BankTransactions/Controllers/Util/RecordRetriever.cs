using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BankTransactions.Controllers.Adapters;
using BankTransactions.Controllers.Util;
using nl.jbs.banktransactions;
using nl.jbs.banktransactions.Models;

namespace BankTransactions.Controllers
{
    public class RecordRetriever
    {
        public static IEnumerable<BankRecord> GetAllBankRecords()
        {
            List<BankRecord> uploadList = new List<BankRecord>();
            using (var db = new BankTransactionsContext())
            {
                foreach (BankTransactionsUpload upload in db.bankTransaction)
                {
                    uploadList.Concat(upload.bankRecord);
                }
            }
            return uploadList;
        }

        public static IEnumerable<BankRecord> GetBankRecordsByUser(String userName)
        {
            List<BankRecord> uploadList = new List<BankRecord>();
            using (var db = new BankTransactionsContext())
            {
                List<BankTransactionsUpload> transactions = db.bankTransaction.Where(x => x.user == userName).ToList<BankTransactionsUpload>();

                foreach (BankTransactionsUpload upload in db.bankTransaction)
                {
                    uploadList.Concat(upload.bankRecord);
                }
            }
            return uploadList;
        }

        public static IEnumerable<BankRecord> GetBankRecordsByUploadPerUser(String path, String userName)
        {
            List<BankRecord> uploadList = new List<BankRecord>();
            using (var db = new BankTransactionsContext())
            {
                List<BankTransactionsUpload> transactions = db.bankTransaction.Where(x => x.user == userName && x.filePath == path).ToList<BankTransactionsUpload>();

                foreach (BankTransactionsUpload upload in transactions)
                {
                    uploadList.Concat(upload.bankRecord);
                }
            }
            return uploadList;
        }
    }
}