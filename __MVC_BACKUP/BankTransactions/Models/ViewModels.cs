using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nl.jorncruijsen.jbs.transactions;

namespace BankTransactions.Models
{
    public class HomeIndexViewModel
    {
        public IList<BankRecord> Selection { get; set; }
    }

    public class HomeDetailsViewModel
    {
        public IList<BankRecord> Selection { get; set; }
    }
}