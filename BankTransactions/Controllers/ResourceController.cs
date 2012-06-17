using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankTransactions.Controllers.Util;
using BankTransactions.Controllers.Adapters;

namespace BankTransactions.Controllers
{
    public class ResourceController : Controller
    {
        public ActionResult Index()
        {
            string resourceFileLocation = ConfigurationManager.BaseLocation + ConfigurationManager.GetSettingValue(ConfigurationManager.FileNameKey);

            ViewData["ResourceFolder"] = resourceFileLocation;
            ViewData["ResourceType"] = TransactionAdapterType.RABOBANK;
            //ViewData["ResourceType"] = ConfigurationManager.GetSettingValue<TransactionAdapterType>(ConfigurationManager.FileTypeKey);
            ViewData["NumRecords"] = RecordRetriever.GetBankRecords().Count();
            ViewData["NumLines"] = System.IO.File.ReadLines(resourceFileLocation).Count();
            ViewData["LastUpdate"] = System.IO.File.GetLastWriteTime(resourceFileLocation);
            ViewData["RootFiles"] = System.IO.Directory.EnumerateFiles(ConfigurationManager.BaseLocation);

            return View();
        }

        /**
         * TODO: Refactor
         */
        public ActionResult AddFile(HttpPostedFileBase file) {
            string resourceFileLocation = ConfigurationManager.BaseLocation + ConfigurationManager.GetSettingValue(ConfigurationManager.FileNameKey);
            TransactionAdapterType type = (TransactionAdapterType)Enum.Parse(typeof(TransactionAdapterType), ConfigurationManager.GetSettingValue(ConfigurationManager.FileTypeKey));

            ViewData["ResourceFolder"] = resourceFileLocation;
            ViewData["ResourceType"] = type.ToString();
            ViewData["NumRecords"] = RecordRetriever.GetBankRecords().Count();
            ViewData["NumLines"] = System.IO.File.ReadLines(resourceFileLocation).Count();
            string path = System.IO.Path.Combine(ConfigurationManager.BaseLocation, "Uploaded/" + DateTime.Now.Ticks + "_" + System.IO.Path.GetFileName(file.FileName));
            file.SaveAs(path);

            ViewData["NewFileNumLines"] = System.IO.File.ReadLines(path).Count();
            ViewData["NewFileNumRecords"] = RecordRetriever.GetBankRecords(path, type).Count;

            CSVMerger.MergeFiles(resourceFileLocation, path);

            ViewData["CombinedFileNumLines"] = System.IO.File.ReadLines(resourceFileLocation).Count();
            ViewData["CombinedFileNumRecords"] = RecordRetriever.GetBankRecords().Count();

            return View();
        }

        public ActionResult UpdateAdapter(string adapter)
        {
            ConfigurationManager.AddSettingValue(ConfigurationManager.FileTypeKey, adapter);

            return View();
        }
    }
}
