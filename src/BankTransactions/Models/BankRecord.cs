using System;
using System.Globalization;

namespace nl.jbs.banktransactions
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
        public string Unknown { get; set; }
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
        }

        public override string ToString()
        {
            return string.Format("Record amounts to EUR {1}, sent to {3}, is labelled [{2}] and was created on {0}", RequestDate.ToString(), Amount, Description, Name);
        }
    }
}