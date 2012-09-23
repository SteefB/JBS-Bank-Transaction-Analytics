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
            // Set containing all records (as unparsed strings)
            HashSet<string> records = new HashSet<string>();

            // Merge the lines in both files to the Set
            AddRecords(original, records);
            AddRecords(input, records);

            // Get original and backup file/directory info
            string directory = Path.GetDirectoryName(original);
            string fileName = Path.GetFileNameWithoutExtension(original);
            string filePath = directory + "/Backups/" + fileName;

            // If the backup directory doesn't exist, create it
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            // Backup the original file
            File.Move(original, filePath + "/" + DateTime.Now.Ticks + "_" + fileName);

            // Store all merged records in the original file
            File.WriteAllLines(original, records);
        }

        private static void AddRecords(string fileLocation, HashSet<string> col)
        {
            // For each line in the given file, add it to the given set
            StreamReader file = new StreamReader(fileLocation);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                col.Add(line);
            }

            // Close the file handle
            file.Close();
        }
    }
}
