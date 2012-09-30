using System;

namespace BankTransactions.Controllers.Util
{
    internal class NoSuchSettingException : Exception
    {
        public NoSuchSettingException(Exception ex)
            : base("This setting does not exist.", ex)
        {
        }
    }
}