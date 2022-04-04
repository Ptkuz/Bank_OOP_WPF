using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baank_Class_Library
{

    public class DemandAccount : Account
    {
        public DemandAccount() { }

        public DemandAccount(decimal sum, int percentage) : base(sum, percentage) { }

        protected internal override void Open()
        {
            base.OnOpened(new AccountAventArgs($"Открыт новый счет до востребования с ID {Id}", Id));
        }

    }
}
