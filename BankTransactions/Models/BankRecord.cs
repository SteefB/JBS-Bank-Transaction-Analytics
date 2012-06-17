using System;
using System.Globalization;

namespace nl.jorncruijsen.jbs.transactions
{
    public class BankRecord
    {
        public enum TransactionType
        {
            DEBIT, CREDIT
        }

        public enum TransactionCategory
        {
            AC, // Acceptgiro
            BA, // Betaalautomaat
            BG, // Bankgiro opdracht
            CB, // Crediteuren betaling
            CK, // Chipknip
            DB, // Diverse boekingen
            EB, // Bedrijven Euro-incasso
            EI, // Euro-incasso
            FB, // FiNBOX
            GA, // Geldautomaat Euro
            GB, // Geldautomaat VV
            ED, // iDEAL
            KH, // Kashandeling
            MA, // Machtiging
            SB, // Salaris betaling
            TB, // Eigen rekening
            TG  // Telegiro
        }

        #region Variables

        private string name;

        #endregion Variables

        #region Properties

        public string BankNum { get; set; }
        public string Currency { get; set; }
        public DateTime RequestDate { get; set; }
        public TransactionType Type { get; set; }
        public double Amount { get; set; }
        public string OtherBankNumber { get; set; }
        public string Name
        {
            get
            {
                return string.IsNullOrWhiteSpace(name) ? OtherBankNumber : name;
            }

            set
            {
                this.name = value;
            }
        }
        public DateTime ExecutionDate { get; set; }
        public TransactionCategory Category { get; set; }
        public string Unknown2 { get; set; }
        public string Description
        {
            get
            {
                return Description1 + Description2 + Description3 + Description4 + Description5 + Description6;
            }
        }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public string Description3 { get; set; }
        public string Description4 { get; set; }
        public string Description5 { get; set; }
        public string Description6 { get; set; }

        #endregion Properties

        public BankRecord()
        {

        }

        public BankRecord(string[] args)
        {
            if (args.Length != 16)
            {
                throw new ArgumentException("Corrupt record, please fix.");
            }

            // First value is the bank number
            BankNum = args[0];

            // Second value is the currency
            Currency = args[1];

            // Third value is the request date
            RequestDate = parseDate(args[2]);

            // Fourth value is the type of transaction // TODO: Refactor neatly
            Type = args[3] == "C" ? TransactionType.CREDIT : TransactionType.DEBIT;

            // Fifth value is the transferred amount
            Amount = Double.Parse(args[4], new CultureInfo("en-US"));

            // Sixth value is the other bank account number (or zero's if there is none)
            OtherBankNumber = args[5];

            // Seventh value is the other bank account name
            Name = args[6].Replace('&', '|');

            // Eigth value is the execution date
            ExecutionDate = parseDate(args[7]);

            // Ninth value is some kind of abbreviation for some magical thing
            TransactionCategory cat;
            Enum.TryParse<TransactionCategory>(args[8].ToUpper(), out cat);
            Category = cat;

            // Tenth value appears to always be empty..
            Unknown2 = args[9];

            // Eleven to sixteen is the description (which could include transaction identifiers and/or exchange rates)
            Description1 = args[10];
            Description2 = args[11];
            Description3 = args[12];
            Description4 = args[13];
            Description5 = args[14];
            Description6 = args[15];

            // System.out.printf(dinger + "\n", (Object[]) args);
        }

        public override string ToString()
        {
            return string.Format("Record amounts to EUR {1}, sent to {3}, is labelled [{2}] and was created on {0} and la", RequestDate.ToString(), Amount, Description, Name);
        }

        private DateTime parseDate(string date)
        {
            if (date.Length != 8)
            {
                throw new ArgumentException("Corrupt date string: " + date);
            }

            return new DateTime(Int16.Parse(date.Substring(0, 4)), Int16.Parse(date.Substring(4, 2)), Int16.Parse(date.Substring(6, 2)));
        }
    }
}