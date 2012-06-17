using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace BankTransactions.Controllers.Util
{
    public class CSVMerger
    {
        public static void MergeFiles(string original, string input)
        {
            HashSet<string> records = new HashSet<string>();

            AddRecords(original, records);
            AddRecords(input, records);

            string directory = Path.GetDirectoryName(original);
            string fileName = Path.GetFileNameWithoutExtension(original);
            string filePath = directory + "/Backups/" + fileName;

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            File.Move(original, filePath + "/" + DateTime.Now.Ticks + "_" + fileName);

            File.WriteAllLines(original, records);
        }

        private static void AddRecords(string fileLocation, HashSet<string> col)
        {
            StreamReader file = new StreamReader(fileLocation);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                col.Add(line);
            }

            file.Close();
        }
    }
}
