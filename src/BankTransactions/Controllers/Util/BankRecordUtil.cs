using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nl.jbs.banktransactions;
using nl.jbs.banktransactions.Models;

namespace BankTransactions.Controllers.Util
{
    public class BankRecordUtil
    {
        public static double getAmount(BankRecord record)
        {
            return record.Amount * (record.Type.Equals(BankRecord.TransactionType.CREDIT) ? 1 : -1);
        }
    }
}