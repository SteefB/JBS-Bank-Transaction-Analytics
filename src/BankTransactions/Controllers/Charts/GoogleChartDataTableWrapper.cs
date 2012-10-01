using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using nl.jbs.banktransactions;

namespace BankTransactions.Controllers.Charts
{
    public class GoogleChartDataTableWrapper
    {
        private const string OBJECT_TEMPLATE = "{{{0},{1}}}";
        private const string COL_TEMPLATE = "cols:[{0}]";
        private const string COL_LINE_TEMPLATE = "{{id:'{0}',label:'{1}',type:'{2}'}}";
        private const string ROW_TEMPLATE = "rows:[{0}]";
        private const string ROW_LINE_TEMPLATE = "{{c:[{0}]}}";
        private const string ROW_DIV_TEMPLATE = "{{v:{0}}}";

        private GoogleChartDataTableWrapper()
        { }

        /**
         * Parses the given rows and returns a JSON string that can be parsed by the Google Charting libraries as a DataTable
         */
        public static string ToJSON(string[] titles, IEnumerable<GoogleChartRow> data)
        {
            // If there are no records, return an empty JSON template
            if (data.Count() == 0)
                return string.Format(OBJECT_TEMPLATE, string.Empty, string.Empty);

            // Parse the data types to the GoogleChartTable cols, uses the first item of the rows to get the data types
            string cols = GetJSONCols(titles, data.First());

            // Parse the data into rows
            string rows = GetJSONRows(data);

            Console.WriteLine(string.Format(OBJECT_TEMPLATE, cols, rows));

            return string.Format(OBJECT_TEMPLATE, cols, rows);
        }

        /**
         * Retrieves the JSON data for the rows
         */
        private static string GetJSONRows(IEnumerable<GoogleChartRow> data)
        {
            // Formats each data row into DataTable JSON rows
            return string.Format(ROW_TEMPLATE, string.Join(",", data.Select(r => string.Format(ROW_LINE_TEMPLATE, string.Join(",", r.Data.Select(d => string.Format(ROW_DIV_TEMPLATE, GetStringData(d))))))));
        }

        private static string GetStringData(object d)
        {
            if (d is DateTime)
            {
                DateTime date = (DateTime)d;
                return string.Format("new Date({0}, {1}, {2})", date.Year, date.Month, date.Day);
            }

            return d.ToString();
        }

        /**
         * Retrieves the JSON data for the cols
         */
        private static string GetJSONCols(string[] titles, GoogleChartRow googleChartRow)
        {
            // Gets the types, converts them to DataTable type strings, joins them with titles (using the counter) and formats them into a single JSON string
            int i = 0;
            return string.Format(COL_TEMPLATE, string.Join(",", googleChartRow.Types.Select(r => string.Format(COL_LINE_TEMPLATE, new object[] { i, titles[i++], GetStringType(r) }))));
        }

        /**
         * Gets the DataTable string type for the given Type ref
         * 
         * FIXME For now, we're only gonna handle DateTime and Number types
         */
        private static string GetStringType(Type r)
        {
            if(r.Equals(typeof(DateTime))) {
                return "date";
            } else {
                return "number";
            }
        }
    }
}