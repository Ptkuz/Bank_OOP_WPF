using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baank_Class_Library
{

    public delegate void AccountStateHandler(object sender, AccountAventArgs e);
    public class AccountAventArgs
    {
        public string Message { get; private set; }
        public decimal Sum { get; private set; }

        public AccountAventArgs(string message, decimal sum) 
        { 
            Message = message;
            Sum = sum;
        }
    }
}
