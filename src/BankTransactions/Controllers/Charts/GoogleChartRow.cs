using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using nl.jbs.banktransactions;
using System.Collections;

namespace BankTransactions.Controllers.Charts
{
    /**
     * Wraps a GoogleChart row
     */
    public class GoogleChartRow
    {
        public object[] Data { get; set; }

        public GoogleChartRow(object[] data)
        {
            Data = data;
        }

        public Type[] Types {
            get {
                return Data.Select(r => r.GetType()).ToArray<Type>();
            }
        }   
    }
}