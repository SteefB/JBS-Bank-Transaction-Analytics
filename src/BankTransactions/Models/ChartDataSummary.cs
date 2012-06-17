using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nl.jorncruijsen.jbs.transactions;

namespace nl.jorncruijsen.jbs.transactions
{
    public class ChartDataSummary
    {
        public IEnumerable<ChartData> AllTransactions;
        public IEnumerable<ChartData> MonthlySpending;
        public IEnumerable<ChartData> MonthlySpendingCumulative;
        public IEnumerable<ChartData> MonthlySpending2011;
        public IEnumerable<ChartData> MonthlySpending2012;
    }
}