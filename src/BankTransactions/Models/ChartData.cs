using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nl.jbs.banktransactions
{
    public class ChartData
    {
        public static List<ChartData> GetMonthlySpending(IOrderedEnumerable<IGrouping<string, BankRecord>> selection)
        {
            var data = new List<ChartData>();

            double cum = 0;

            foreach (IGrouping<string, BankRecord> group in selection)
            {
                double monthlyDif = group.Sum(r => (r.Type.Equals(BankRecord.TransactionType.CREDIT) ? 1 : -1) * r.Amount);

                cum += monthlyDif;

                data.Add(new ChartData() {
                    Label = group.Key,
                    Value1 = group.Where(r => r.Type.Equals(BankRecord.TransactionType.DEBIT)).Sum(r => r.Amount),
                    Value2 = group.Where(r => r.Type.Equals(BankRecord.TransactionType.CREDIT)).Sum(r => r.Amount),
                    Value3 = monthlyDif,
                    Value4 = group.Sum(r => r.Amount),
                    Value5 = cum 
                });
            }

            return data;
        }


        public static List<ChartData> GetMonthlySpendingCumulative(IOrderedEnumerable<IGrouping<string, BankRecord>> selection)
        {
            var data = new List<ChartData>();

            double cumCredit = 0;
            double cumDedit = 0;

            foreach (IGrouping<string, BankRecord> group in selection)
            {
                cumCredit += group.Where(r => r.Type.Equals(BankRecord.TransactionType.CREDIT)).Sum(r => r.Amount);
                cumDedit += group.Where(r => r.Type.Equals(BankRecord.TransactionType.DEBIT)).Sum(r => r.Amount);

                data.Add(new ChartData()
                {
                    Label = group.Key,
                    Value1 = cumCredit,
                    Value2 = cumDedit

                });
            }

            return data;
        }

        public static IEnumerable<ChartData> GetAllTransactionData(IEnumerable<BankRecord> selection)
        {
            var data = new List<ChartData>();

            double funding = 0;

            foreach (BankRecord rec in selection)
            {
                int dif = rec.Type.Equals(BankRecord.TransactionType.CREDIT) ? 1 : -1;

                funding += rec.Amount * dif;

                data.Add(new ChartData()
                {
                    Date = rec.RequestDate,
                    Value1 = funding,
                    Value2 = rec.Amount * dif,

                });
            }

            return data;
        }

        public static IEnumerable<ChartData> GetAnnualSpending(IEnumerable<BankRecord> selection)
        {
            if (selection.Count() == 0)
            {
                return new List<ChartData>();
            }

            IEnumerable<ChartData> months = GetMonthlySpending(selection.GroupBy(r => new TimePeriodGroup(r.RequestDate).ToString()).OrderByDescending(r => r.Key));

            int year = selection.First().RequestDate.Year;

            // TODO: Find a proper way to do a left outer join
            List<ChartData> leftOvers = new List<ChartData>(12 - months.Count());
            for(int i = 12; i > months.Count();i--) {
                leftOvers.Add(new ChartData()
                {
                    Label = string.Format("{1}/{0:00}", i, year)
                });
            }

            months = leftOvers.Concat(months);

            return months;
        }

        public string Label { get; set; }
        public DateTime Date { get; set; }
        public double Value1 { get; set; }
        public double Value2 { get; set; }
        public double Value3 { get; set; }
        public double Value4 { get; set; }
        public double Value5 { get; set; }
    }
}