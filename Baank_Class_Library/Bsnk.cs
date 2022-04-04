namespace Baank_Class_Library
{
    public enum AccountType
    {
        Ordinary,
        Deposit

    }

    public class Bank<T> where T : Account
    {
       public T newAccount = null;
        public List<T> accounts = new List<T>();
        public string Name { get; private set; }

        public Bank(string name)
        {
            Name = name;
        }

        public Account Open(AccountType accountType, decimal sum,
                            AccountStateHandler addSumHandler, AccountStateHandler withoutSumHandler,
                            AccountStateHandler calculationHandler, AccountStateHandler closeAccountHandler,
                            AccountStateHandler openHandler)
        {

           
            switch (accountType)
            {
                case AccountType.Ordinary:
                    newAccount = new DemandAccount(sum, 1) as T;
                    break;
                case AccountType.Deposit:
                    newAccount = new DepositAccount(sum, 40) as T;
                    break;


            }

            if (newAccount == null)
                throw new NullReferenceException("Ошибка создания счета");

            accounts.Add(newAccount);

            newAccount.eventPut += addSumHandler;
            newAccount.eventWithout += withoutSumHandler;
            newAccount.eventClose += closeAccountHandler;
            newAccount.eventCalculate += calculationHandler;
            newAccount.eventOpen += openHandler;


            newAccount.Open();
            return newAccount;
        }

        public T FindAccount(int id)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].Id == id)
                    return accounts[i];
            }
            return null;
        }


        public T FindAccount(int id, out int index)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].Id == id)
                {
                    index = i;
                    return accounts[i];
                }
            }
            index = -1;
            return null;
        }

        public void Put(decimal sum, int id)
        {
            T account = FindAccount(id);
            if (account == null)
                throw new NullReferenceException("Счет не найден");
            else
                account.Put(sum);

        }

        public void Without(decimal sum, int id)
        {
            T account = FindAccount(id);
            if (account == null)
                throw new NullReferenceException("Счет не найден");
            else
                account.Without(sum);

        }

        public int Close(int id)
        {
            int index;
            T account = FindAccount(id, out index);
            if (account == null)
                throw new NullReferenceException("Счет не найден");
            account.Close();

            accounts.RemoveAt(index);
            return index;


            return -1;

        }

        public void CalculatePercantage(Account account)
        {
           
                account.Caclulate();

          

        }






    }
}
