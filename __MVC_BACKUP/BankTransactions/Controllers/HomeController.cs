using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.ObjectModel;
using nl.jorncruijsen.jbs.transactions;
using Microsoft.VisualBasic.FileIO;
using BankTransactions.Models;

namespace BankTransactions.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private const string FILE_LOCATION = @"C:\Program Files\_WORKSPACE\Eclipse\CSVTest\mut.txt";

        public ActionResult Index()
        {
            IList<BankRecord> selection = ParseBankRecords(FILE_LOCATION).OrderByDescending(r => r.ExecutionDate).ToList();

            return View(selection);
        }

        public ActionResult Details(string name)
        {
            IList<BankRecord> selection = ParseBankRecords(FILE_LOCATION).Where(r => r.Name.ToLower().Contains("donalds")).OrderByDescending(r => r.ExecutionDate).ToList();

            return View(selection);
        }

        public ActionResult Overview()
        {
            IOrderedEnumerable<IGrouping<string, BankRecord>> selection = ParseBankRecords(FILE_LOCATION).GroupBy(r => r.Name).OrderByDescending(g => g.Sum(r => r.Amount));

            return View(selection);
        }

        private static IList<BankRecord> ParseBankRecords(string file)
        {
            IList<BankRecord> records = new ObservableCollection<BankRecord>();

            TextFieldParser parser = new TextFieldParser(file);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            while (!parser.EndOfData)
            {
                records.Add(new BankRecord(parser.ReadFields()));
            }

            parser.Close();

            return records;
        }
    }
}
