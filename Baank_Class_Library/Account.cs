namespace Baank_Class_Library
{

    public abstract class Account : IAccount
    {

        // Событие на добавление денег на счет
        protected internal event AccountStateHandler eventPut;
        // Событие на снятие денег со счета
        protected internal event AccountStateHandler eventWithout;
        // Событие при открытии счета
        protected internal event AccountStateHandler eventOpen;
        // Событие при закрытии счета
        protected internal event AccountStateHandler eventClose;
        // Событие при расчете процентов
        protected internal event AccountStateHandler eventCalculate;


        static int counter = 0;
        public int Days { get; set; }

        public decimal Sum { get; private set; }
        public int Percentage { get; private set; }
        public int Id { get; private set; }

        public Account() { }
        
        public Account(decimal sum, int percentage)
        {
            Sum = sum;
            Percentage = percentage;
            Id = ++counter;
            Days = 0;
        }

        private void CallEvent(AccountAventArgs e, AccountStateHandler handler) 
        {
            if (e!=null)
                handler.Invoke(this, e);
        }

        protected virtual void OnOpened(AccountAventArgs e) 
        {
            CallEvent(e, eventOpen);
        
        }

        protected virtual void OnAdded(AccountAventArgs e)
        {
            CallEvent(e, eventPut);

        }
        protected virtual void OnWithout(AccountAventArgs e)
        {
            CallEvent(e, eventWithout);

        }
        protected virtual void OnClosed(AccountAventArgs e)
        {
            CallEvent(e, eventClose);

        }
        protected virtual void OnCalculated(AccountAventArgs e)
        {
            CallEvent(e, eventCalculate);

        }


        public virtual void Put(decimal sum)
        {
            Sum += sum;
            OnAdded(new AccountAventArgs("На счет поступила сумма " + sum, sum));
        }

        public virtual decimal Without(decimal sum)
        {
            decimal result = 0;
            if (Sum >= sum)
            {
                Sum -= sum;
                result = Sum;
                OnWithout(new AccountAventArgs("Со счета " + Id + " списали " + sum, sum));


            }
            else
            {
                OnWithout(new AccountAventArgs("Недостаточно денег на счете " + Id + " для списания " + sum + 
                    ". на счете всего " + Sum, 0));
            }
            return result;
        }

        protected internal virtual void Open()
        {
            OnOpened(new AccountAventArgs("Открыт новый счет с ID " + Id, Sum));

        }

        protected internal virtual void Close()
        {
            OnClosed(new AccountAventArgs("Счет с ID " + Id + " закрыт. Итоговая сумма: " + Sum, Sum));
        }

        protected internal void IncrementDays()
        {
            Days++;
        }

        protected internal virtual void Caclulate()
        {
            decimal increment = Sum * Percentage / 100;
            Sum += increment;
            OnCalculated(new AccountAventArgs("На счет с ID " + Id + " начислены проценты " + increment, increment));

        }


    }
}
