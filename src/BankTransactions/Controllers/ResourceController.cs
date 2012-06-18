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
            string resourceFileLocation = ConfigurationManager.BaseLocation + ConfigurationManager.FileName;

            ViewData["ResourceFolder"] = resourceFileLocation;
            ViewData["ResourceType"] = ConfigurationManager.FileType;
            ViewData["NumRecords"] = RecordRetriever.GetBankRecords().Count();
            ViewData["NumLines"] = System.IO.File.ReadLines(resourceFileLocation).Count();
            ViewData["LastUpdate"] = System.IO.File.GetLastWriteTime(resourceFileLocation);

            // Get all file names in the root file directory
            // TODO Use Path or something to properly extract the filename
            ViewData["RootFiles"] = System.IO.Directory.EnumerateFiles(ConfigurationManager.BaseLocation).Select(r => r.Substring(r.LastIndexOf('/') + 1));

            return View();
        }

        /**
         * Updates the root file type
         **/
        public ActionResult UpdateRootFileType(string newResourceType)
        {
            // Get current and parse new type
            TransactionAdapterType currentType = ConfigurationManager.FileType;
            TransactionAdapterType newType = (TransactionAdapterType)Enum.Parse(typeof(TransactionAdapterType), newResourceType);

            // Check equality
            if(!currentType.Equals(newType)) {
                // Save type to settings
                ConfigurationManager.UpdateSettingValue(ConfigurationManager.FileTypeKey, newType);
            }

            // Display index
            return RedirectToAction("Index");
        }

        /***
         * Change the resource file
         */
        public ActionResult UpdateRootFile(string newResourceFileLocation)
        {
            string resourceFileLocation = ConfigurationManager.BaseLocation + ConfigurationManager.FileName;

            // First check if the file is different from the file used
            if (!resourceFileLocation.Equals(newResourceFileLocation))
            {
                // Save filename to settings
                ConfigurationManager.UpdateSettingValue(ConfigurationManager.FileNameKey, newResourceFileLocation);
            }

            // Display index
            return RedirectToAction("Index");
        }

        /**
         * TODO: Refactor
         */
        public ActionResult AddFile(HttpPostedFileBase file) {
            // Get the current resource root file
            string resourceFileLocation = ConfigurationManager.BaseLocation + ConfigurationManager.FileName;
            TransactionAdapterType type = ConfigurationManager.FileType;

            // Push some data to the view
            ViewData["ResourceFolder"] = resourceFileLocation;
            ViewData["ResourceType"] = type.ToString();
            ViewData["NumRecords"] = RecordRetriever.GetBankRecords().Count();
            ViewData["NumLines"] = System.IO.File.ReadLines(resourceFileLocation).Count();

            // Save the uploaded file to the /Uploaded/ dir
            string path = System.IO.Path.Combine(ConfigurationManager.BaseLocation, "Uploaded/" + DateTime.Now.Ticks + "_" + System.IO.Path.GetFileName(file.FileName));
            file.SaveAs(path);

            // Some more data on the new file
            ViewData["NewFileNumLines"] = System.IO.File.ReadLines(path).Count();
            ViewData["NewFileNumRecords"] = RecordRetriever.GetBankRecords(path, type).Count;

            // Merge uploaded file with the root file (which will make an automatic back-up as well)
            CSVMerger.MergeFiles(resourceFileLocation, path);

            // And some more data on the merged files
            ViewData["CombinedFileNumLines"] = System.IO.File.ReadLines(resourceFileLocation).Count();
            ViewData["CombinedFileNumRecords"] = RecordRetriever.GetBankRecords().Count();

            return View();
        }
    }
}
