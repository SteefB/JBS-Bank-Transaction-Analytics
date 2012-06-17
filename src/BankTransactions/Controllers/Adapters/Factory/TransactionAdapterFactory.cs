﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankTransactions.Controllers.Adapters
{
    public class TransactionAdapterFactory
    {
        private List<TransactionAdapter> factories = new List<TransactionAdapter>();
        public static TransactionAdapter GetAdapter(TransactionAdapterType type)
        {
            switch (type)
            {
                case TransactionAdapterType.RABOBANK:
                    return new TransactionAdapterRabobank();
                case TransactionAdapterType.ING:
                    return new TransactionAdapterING();
                default:
                    return null;
            }
        }
    }
}