using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace nl.jbs.banktransactions.Models
{
    public class BankTransactionsContext : DbContext
    {
        public DbSet<BankTransactionsUpload> bankTransaction;
    }
}