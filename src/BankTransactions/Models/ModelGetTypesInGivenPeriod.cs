﻿using nl.jorncruijsen.jbs.transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankTransactions.Models
{
    public class ModelGetTypesInGivenPeriod
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public IOrderedEnumerable<IGrouping<string, BankRecord>> bankrecords { get; set; }

    }
}