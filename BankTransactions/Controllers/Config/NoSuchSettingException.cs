using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankTransactions.Controllers.Util
{
    class NoSuchSettingException : Exception
    {
        public NoSuchSettingException(Exception ex) : base("This setting does not exist.", ex)
        {
        }
    }
}
