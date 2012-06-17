using System;
using System.Globalization;

namespace nl.jorncruijsen.jbs.transactions
{
    public class BankRecord
    {
        public enum TRANSACTION_TYPE
        {
            DEBIT, CREDIT
        };

        #region Variables

        private string bankNum;
        private string currency;
        private DateTime requestDate;
        private TRANSACTION_TYPE type;
        private double amount;
        private string otherBankNumber;
        private string name;
        private DateTime executionDate;
        private string unknown1;
        private string unknown2;
        private string description1;
        private string description2;
        private string description3;
        private string description4;
        private string description5;
        private string description6;

        #endregion Variables

        #region Properties

        public string BankNum
        {
            get { return bankNum; }
            set { bankNum = value; }
        }
        public string Currency
        {
            get { return currency; }
            set { currency = value; }
        }
        public DateTime RequestDate
        {
            get { return requestDate; }
            set { requestDate = value; }
        }
        public TRANSACTION_TYPE Type
        {
            get { return type; }
            set { type = value; }
        }
        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        public string OtherBankNumber
        {
            get { return otherBankNumber; }
            set { otherBankNumber = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public DateTime ExecutionDate
        {
            get { return executionDate; }
            set { executionDate = value; }
        }
        public string Unknown1
        {
            get { return unknown1; }
            set { unknown1 = value; }
        }
        public string Unknown2
        {
            get { return unknown2; }
            set { unknown2 = value; }
        }
        public string Description1
        {
            get { return description1; }
            set { description1 = value; }
        }
        public string Description2
        {
            get { return description2; }
            set { description2 = value; }
        }
        public string Description3
        {
            get { return description3; }
            set { description3 = value; }
        }
        public string Description4
        {
            get { return description4; }
            set { description4 = value; }
        }
        public string Description5
        {
            get { return description5; }
            set { description5 = value; }
        }
        public string Description6
        {
            get { return description6; }
            set { description6 = value; }
        }

        #endregion Properties

        public BankRecord(string[] args)
        {
            if (args.Length != 16)
            {
                throw new ArgumentException("Corrupt record, please fix.");
            }

            // First value is the bank number
            bankNum = args[0];

            // Second value is the currency
            currency = args[1];

            // Third value is the request date
            requestDate = parseDate(args[2]);

            // Fourth value is the type of transaction
            type = args[3] == "C" ? TRANSACTION_TYPE.CREDIT : TRANSACTION_TYPE.DEBIT;

            // Fifth value is the transferred amount
            amount = Double.Parse(args[4], new CultureInfo("en-US"));

            // Sixth value is the other bank account number (or zero's if there is none)
            otherBankNumber = args[5];

            // Seventh value is the other bank account name
            name = args[6].Replace('&', '|');

            // Eigth value is the execution date
            executionDate = parseDate(args[7]);

            // Ninth value is some kind of abbreviation for some magical thing
            unknown1 = args[8];

            // Tenth value appears to always be empty..
            unknown2 = args[9];

            // Eleven to sixteen is the description (which could include transaction identifiers and/or exchange rates)
            description1 = args[10];
            description2 = args[11];
            description3 = args[12];
            description4 = args[13];
            description5 = args[14];
            description6 = args[15];

            // System.out.printf(dinger + "\n", (Object[]) args);
        }

        public override string ToString()
        {
            return string.Format("Record amounts to EUR {1}, sent to {3}, is labelled [{2}] and was created on {0} and la", requestDate.ToString(), amount, description1, name);
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