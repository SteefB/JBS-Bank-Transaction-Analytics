
using nl.jbs.banktransactions;
using System.Collections.Generic;
using System.Text;

namespace BankTransactions.Controllers.Charts
{
    public class GoogleChartDataTableProvider
    {
        public static string GetChartDataFor(IEnumerable<BankRecord> records)
        {
            StringBuilder sb = new StringBuilder("{"
                         + "cols: ["
                            + "{id: 'startDate', label: 'Datum', type: 'date'},"
                            + "{id: 'amount', label: 'Amount', type: 'number'}"
                        + "],"
                         + "rows: [");

            foreach(BankRecord rec in records) {
                //new Date(2008, 1, 26)
                //sb.Append("{c:[{v: " + rec.Amount + "}, {v: new Date(" + rec.RequestDate.Year + ", " + rec.RequestDate.Month + ", " + rec.RequestDate.Day + "), f:'" + rec.RequestDate.ToShortDateString() + "'}]},");
                sb.Append(string.Format("{{c:[{{v: new Date({1}, {2}, {3})}}, {{v: {0}}}]}},", 
                    rec.Amount, 
                    rec.RequestDate.Year, 
                    rec.RequestDate.Month, 
                    rec.RequestDate.Day));

            }
                        sb.Append("]"
                    + "}");

            return sb.ToString();
            
                            //"{c:[{v: 35}, {v: new Date(2008, 1, 26), f:'February 26, 2008'}]},"
            return "{"
                        + "cols: [{id: 'A', label: 'NEW A', type: 'string'},"
                            + "{id: 'B', label: 'B-label', type: 'number'},"
                            + "{id: 'C', label: 'C-label', type: 'date'}"
                        + "],"
                        + "rows: [{c:[{v: 'a'}, {v: 1.0, f: 'One'}, {v: new Date(2008, 1, 28, 0, 31, 26), f: '2/28/08 12:31 AM'}]},"
                            + "{c:[{v: 'b'}, {v: 2.0, f: 'Two'}, {v: new Date(2008, 2, 30, 0, 31, 26), f: '3/30/08 12:31 AM'}]},"
                            + "{c:[{v: 'c'}, {v: 3.0, f: 'Three'}, {v: new Date(2008, 3, 30, 0, 31, 26), f: '4/30/08 12:31 AM'}]}"
                        + "],"
                        + "p: {foo: 'hello', bar: 'world!'}"
                    + "}";

            return "{"
                        + "cols: [{id: 'A', label: 'NEW A', type: 'string'},"
                            + "{id: 'B', label: 'B-label', type: 'number'},"
                            + "{id: 'C', label: 'C-label', type: 'date'}"
                        + "],"
                        + "rows: [{c:[{v: 'a'}, {v: 1.0, f: 'One'}, {v: new Date(2008, 1, 28, 0, 31, 26), f: '2/28/08 12:31 AM'}]},"
                            + "{c:[{v: 'b'}, {v: 2.0, f: 'Two'}, {v: new Date(2008, 2, 30, 0, 31, 26), f: '3/30/08 12:31 AM'}]},"
                            + "{c:[{v: 'c'}, {v: 3.0, f: 'Three'}, {v: new Date(2008, 3, 30, 0, 31, 26), f: '4/30/08 12:31 AM'}]}"
                        + "],"
                        + "p: {foo: 'hello', bar: 'world!'}"
                    + "}";
        }

        public static string GetChartOptionsFor(string title, int width, int height)
        {
            return string.Format("{{'title': '{0}','width': {1},'height': {2}}}", title, width, height);
        }
    }
}