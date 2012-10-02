using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankTransactions.Controllers.Util;
using nl.jbs.banktransactions;
using nl.jbs.banktransactions.Models;

namespace BankTransactions.Controllers.Charts
{
    public class GoogleChartDataTableProvider
    {
        public static string GetBasicChartDataFor(IEnumerable<BankRecord> records)
        {
            double acc = 0;
            return GoogleChartDataTableWrapper.ToJSON(new string[] { "Date", "Amount", "Total" },
                records.OrderBy(r => r.RequestDate).Select(r => new GoogleChartRow(new object[] { r.RequestDate, r.Amount, acc += r.CreditAmount })));
        }

        private static double GetCreditAmount(BankRecord r)
        {
            throw new System.NotImplementedException();
        }

        public static string GetChartOptionsFor(string title, int width, int height)
        {
            return string.Format("{{'title': '{0}','width': {1},'height': {2}, 'seriesType': 'bars', series: {{0: {{type: 'bar', groupWidth: 20 }}}}, series: {{1: {{type: 'line', lineWidth: 2}}}}}}", title, width, height);
        }
    }
}