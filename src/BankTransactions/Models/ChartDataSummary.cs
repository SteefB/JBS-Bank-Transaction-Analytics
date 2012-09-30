using System.Collections.Generic;

namespace nl.jbs.banktransactions
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