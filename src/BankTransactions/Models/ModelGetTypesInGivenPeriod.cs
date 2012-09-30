using nl.jbs.banktransactions;
using System;
using System.Linq;

namespace BankTransactions.Models
{
    public class ModelGetTypesInGivenPeriod
    {
        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public IOrderedEnumerable<IGrouping<string, BankRecord>> bankrecords { get; set; }
    }
}