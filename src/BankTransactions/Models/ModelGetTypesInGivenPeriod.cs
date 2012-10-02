using System;
using System.Linq;
using nl.jbs.banktransactions;

namespace nl.jbs.banktransactions.Models
{
    public class ModelGetTypesInGivenPeriod
    {
        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public IOrderedEnumerable<IGrouping<string, BankRecord>> bankrecords { get; set; }
    }
}