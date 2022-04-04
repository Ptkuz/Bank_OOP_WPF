using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baank_Class_Library
{

    public class DepositAccount : Account
    {
        public DepositAccount() { }

        public DepositAccount(decimal sum, int percentage) : base(sum, percentage) 
        { 
        }

        protected internal override void Open()
        {
            base.OnOpened(new AccountAventArgs($"Открыт новый депозитный счет с ID: {Id}", Id));
        }

        public override void Put(decimal sum)
        {
            if (Days % 30 == 0)
            {
                base.Put(sum);
            }
            else 
            {
                base.OnAdded(new AccountAventArgs($"На счет можно класть только каждые 30 дней", 0));
            
            }
        }

        public override decimal Without(decimal sum)
        {
            if (Days % 30 == 0)
            {
                return base.Without(sum);
            }
            else 
            {
                base.OnWithout(new AccountAventArgs($"Вывести деньги можно только каждые 30 дней", 0));
            
            }

            return 0;

        }

        protected internal override void Caclulate()
        {
            if(Days%30==0)
                base.Caclulate();
        }

    }
}
