using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.ObjectModel;
using nl.jorncruijsen.jbs.transactions;
using Microsoft.VisualBasic.FileIO;
using JQChart.Web;

namespace BankTransactions.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult All()
        {
            IEnumerable<BankRecord> selection = RecordRetriever.GetBankRecords().OrderByDescending(r => r.RequestDate);

            return View(selection);
        }

        public ActionResult ATM()
        {
            IEnumerable<BankRecord> selection = RecordRetriever.GetBankRecords().Where(r => r.Description.Contains("Geldautomaat")).OrderByDescending(r => r.RequestDate);

            return View(selection);
        }

        public ActionResult Author()
        {
            return View();
        }

        public ActionResult Charts()
        {
            IEnumerable<BankRecord> selection = RecordRetriever.GetBankRecords();

            IOrderedEnumerable<IGrouping<string, BankRecord>> monthly = selection.GroupBy(r => new TimePeriodGroup(r.RequestDate).ToString()).OrderBy(r => r.Key);

            ChartDataSummary cds = new ChartDataSummary() {
                AllTransactions = ChartData.GetAllTransactionData(selection),
                MonthlySpending = ChartData.GetMonthlySpending(monthly),
                MonthlySpendingCumulative = ChartData.GetMonthlySpendingCumulative(monthly),
                MonthlySpending2011 = ChartData.GetAnnualSpending(selection.Where(r => r.RequestDate.Year.Equals(2011))),
                MonthlySpending2012 = ChartData.GetAnnualSpending(selection.Where(r => r.RequestDate.Year.Equals(2012)))
            };

            return View(cds);
        }

        public ActionResult Config()
        {
            return View();
        }

        public ActionResult Details(string name)
        {
            IEnumerable<BankRecord> selection = RecordRetriever.GetBankRecords().Where(r => r.Name.ToLower().Contains(name.ToLower())).OrderByDescending(r => r.RequestDate);

            return View(selection);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Monthly()
        {
            IOrderedEnumerable<IGrouping<string, BankRecord>> selection = RecordRetriever.GetBankRecords().GroupBy(r => new TimePeriodGroup(r.RequestDate).ToString()).OrderByDescending(r => r.Key);

            return View(selection);
        }

        public ActionResult Overview()
        {
            IOrderedEnumerable<IGrouping<string, BankRecord>> selection = RecordRetriever.GetBankRecords().GroupBy(r => r.Name).OrderByDescending(g => g.Sum(r => r.Amount));

            return View(selection);
        }

        public ActionResult PoS()
        {
            IEnumerable<BankRecord> selection = RecordRetriever.GetBankRecords().Where(r => r.Description.Contains("Betaalautomaat")).OrderByDescending(r => r.RequestDate);

            return View(selection);
        }

        public ActionResult Recent()
        {
            IEnumerable<BankRecord> rec = RecordRetriever.GetBankRecords();

            return View(rec.Where(r => r.RequestDate.AddDays(7).CompareTo(rec.OrderByDescending(a => a.RequestDate).First().RequestDate) > 0).OrderByDescending(r => r.RequestDate));
        }

        public ActionResult Stats()
        {
            return All();
        }

        public ActionResult Types()
        {
            IOrderedEnumerable<IGrouping<string, BankRecord>> selection = RecordRetriever.GetBankRecords().GroupBy(r => r.Category.ToString()).OrderByDescending(g => g.Sum(r => r.Amount));

            return View(selection);
        }
    }
}
