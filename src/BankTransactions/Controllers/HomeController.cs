using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using nl.jbs.banktransactions;
using nl.jbs.banktransactions.Models;

namespace BankTransactions.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult All()
        {
            IEnumerable<BankRecord> selection = RecordRetriever.GetAllBankRecords();

            return View(selection);
        }

        public ActionResult ATM()
        {
            IEnumerable<BankRecord> selection = RecordRetriever.GetAllBankRecords().Where(r => r.Description.Contains("Geldautomaat"));

            return View(selection);
        }

        public ActionResult Author()
        {
            return View();
        }

        public ActionResult Charts()
        {
            IEnumerable<BankRecord> selection = RecordRetriever.GetAllBankRecords();

            IOrderedEnumerable<IGrouping<string, BankRecord>> monthly = selection.GroupBy(r => new TimePeriodGroup(r.RequestDate).ToString()).OrderBy(r => r.Key);

            ChartDataSummary cds = new ChartDataSummary()
            {
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
            IEnumerable<BankRecord> selection = RecordRetriever.GetAllBankRecords().Where(r => r.Name.ToLower().Contains(name.ToLower()));

            return View(selection);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Monthly()
        {
            IOrderedEnumerable<IGrouping<string, BankRecord>> selection = RecordRetriever.GetAllBankRecords().GroupBy(r => new TimePeriodGroup(r.RequestDate).ToString()).OrderByDescending(r => r.Key);

            return View(selection);
        }

        public ActionResult Overview()
        {
            IOrderedEnumerable<IGrouping<string, BankRecord>> selection = RecordRetriever.GetAllBankRecords().GroupBy(r => r.Name).OrderByDescending(g => g.Sum(r => r.Amount));

            return View(selection);
        }

        public ActionResult PoS()
        {
            IEnumerable<BankRecord> selection = RecordRetriever.GetAllBankRecords().Where(r => r.Description.Contains("Betaalautomaat"));

            return View(selection);
        }

        public ActionResult Recent()
        {
            IEnumerable<BankRecord> rec = RecordRetriever.GetAllBankRecords();

            return View(rec.Where(r => r.RequestDate.AddDays(7).CompareTo(rec.OrderByDescending(a => a.RequestDate).First().RequestDate) > 0));
        }

        public ActionResult Stats()
        {
            return All();
        }

        public ActionResult Types()
        {
            IOrderedEnumerable<IGrouping<string, BankRecord>> selection = RecordRetriever.GetAllBankRecords().GroupBy(r => r.Category.ToString()).OrderByDescending(g => g.Sum(r => r.Amount));

            return View(selection);
        }

        # region GetTypesInGivenPeriod

        [HttpGet]
        public ActionResult GetTypesInGivenPeriod() // give all transactions from past year as default
        {
            ModelGetTypesInGivenPeriod model = new ModelGetTypesInGivenPeriod
            {
                startDate = DateTime.Now.AddYears(-1),
                endDate = DateTime.Now
            };

            //retrieve all transactions
            IEnumerable<BankRecord> rec = RecordRetriever.GetAllBankRecords();

            // select between dates
            rec = rec.Where(r => r.RequestDate >= model.startDate && r.RequestDate <= model.endDate);

            // group the selection
            IOrderedEnumerable<IGrouping<string, BankRecord>> selection = rec.GroupBy(r => r.Category.ToString()).OrderByDescending(g => g.Sum(r => r.Amount));

            model.bankrecords = selection;

            return View(model);
        }

        [HttpPost]
        public ActionResult GetTypesInGivenPeriod(DateTime startDate, DateTime endDate)
        {
            //retrieve all transactions
            IEnumerable<BankRecord> rec = RecordRetriever.GetAllBankRecords();

            // select between dates
            rec = rec.Where(r => r.RequestDate >= startDate && r.RequestDate <= endDate);

            // group the selection
            IOrderedEnumerable<IGrouping<string, BankRecord>> selection = rec.GroupBy(r => r.Category.ToString()).OrderByDescending(g => g.Sum(r => r.Amount));

            ModelGetTypesInGivenPeriod model = new ModelGetTypesInGivenPeriod
            {
                startDate = startDate,
                endDate = endDate,
                bankrecords = selection
            };

            // end of testing
            return View(model);
        }

        #endregion
    }
}