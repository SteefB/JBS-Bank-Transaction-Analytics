using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using nl.jbs.banktransactions;

namespace nl.jbs.banktransactions.Models
{
    public class BankTransactionsUpload
    {
        [Key]
        public String fileName { get; set; }

        public String filePath { get; set; }

        public String user { get; set; }

        public List<BankRecord> bankRecord;
    }
}