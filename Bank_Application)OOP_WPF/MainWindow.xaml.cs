using Baank_Class_Library;
using System;
using System.Collections;
using System.IO;
using System.Windows;
using System.Windows.Controls;



namespace Bank_Application_OOP_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool flagTextBox = false;
        bool flagRadio = false;
        bool flagDataGrid = false;

        public bool FlagTextBox
        {
            get { return flagTextBox; }
            set
            {
                flagTextBox = value;
                if (flagRadio && flagTextBox)
                    AddAccount.IsEnabled = true;
                else
                    AddAccount.IsEnabled = false;

                if (flagDataGrid && flagTextBox)
                {
                    btn_without.IsEnabled = true;
                    btn_add.IsEnabled = true;
                }
                else
                {
                    btn_without.IsEnabled = false;
                    btn_add.IsEnabled = false;
                }



            }
        }
        public bool FlagRadio
        {
            get { return flagRadio; }
            set
            {
                flagRadio = value;
                if (flagRadio && flagTextBox)
                    AddAccount.IsEnabled = true;
                else
                    AddAccount.IsEnabled = false;
            }
        }

        public bool FlagDataGrid
        {
            get { return flagDataGrid; }
            set
            {
                flagDataGrid = value;
                if (flagDataGrid)
                    CloseAccount.IsEnabled = true;
                else
                    CloseAccount.IsEnabled = false;

                if (flagDataGrid && flagTextBox)
                {
                    btn_without.IsEnabled = true;
                    btn_add.IsEnabled = true;
                }

                else
                {
                    btn_without.IsEnabled = false;
                    btn_add.IsEnabled = false;
                }
            }
        }



        Bank<Account> bank;
        ArrayList arrayList;

        //Account account = null;



        public MainWindow()
        {
            InitializeComponent();
            bank = new Bank<Account>("СуперБанк");
            arrayList = new ArrayList();
        }

        public static void OpenAccountHandler(object sender, AccountAventArgs e)
        {
            MessageBox.Show(e.Message);
        }

        public static void AddAccountHandler(object sender, AccountAventArgs e)
        {
            MessageBox.Show(e.Message);
        }

        public static void WithoutAccountHandler(object sender, AccountAventArgs e)
        {
            MessageBox.Show(e.Message);
        }

        public static void CloseAccountHandler(object sender, AccountAventArgs e)
        {
            MessageBox.Show(e.Message);
        }

        public static void CalculateAccountHandler(object sender, AccountAventArgs e)
        {
            MessageBox.Show(e.Message);
        }

        static void ViewDataGrid(DataGrid dataGridAccounts, ArrayList arrayList)
        {
            dataGridAccounts.Items.Clear();
            for (int i = 0; i < arrayList.Count; i++)
                dataGridAccounts.Items.Add(arrayList[i]);

        }


        private void AddAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                bool boolSum = decimal.TryParse(txbx_money.Text, out decimal summ);
                AccountType accountType = 0;

                if (radioDeposit.IsChecked == true)
                    accountType = AccountType.Deposit;
                else if (radioVostreb.IsChecked == true)
                    accountType = AccountType.Ordinary;

                if (boolSum)
                {
                    bank.newAccount = bank.Open(accountType, summ,
                        OpenAccountHandler, AddAccountHandler, WithoutAccountHandler,
                        CloseAccountHandler, CalculateAccountHandler);

                    arrayList.Add(bank.newAccount);
                    ViewDataGrid(dataGridAccounts, arrayList);

                }
                else
                    throw new Exception("Поле заполненно некорректно");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }

        }

        private void CloseAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = 0;
                object item = dataGridAccounts.SelectedItem;
                if (item != null)
                {
                    string ID = (dataGridAccounts.SelectedCells[0].Column.GetCellContent(item) as TextBlock)!.Text;
                    bool boolID = int.TryParse(ID, out int int_id);
                    if (boolID)
                    {
                        index = bank.Close(int_id);
                    }


                    arrayList.RemoveAt(index);
                    ViewDataGrid(dataGridAccounts, arrayList);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object item = dataGridAccounts.SelectedItem;


                if (item != null)
                {
                    string ID = (dataGridAccounts.SelectedCells[0].Column.GetCellContent(item) as TextBlock)!.Text;
                    bool boolID = int.TryParse(ID, out int int_id);
                    bool boolSum = decimal.TryParse(txbx_money.Text, out decimal summ);
                    if (boolID)
                    {
                        bank.Put(summ, int_id);
                    }
                    else
                        throw new Exception("Произошла ошибка");
                    ViewDataGrid(dataGridAccounts, arrayList);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void btn_without_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object item = dataGridAccounts.SelectedItem;


                if (item != null)
                {
                    string ID = (dataGridAccounts.SelectedCells[0].Column.GetCellContent(item) as TextBlock)!.Text;
                    bool boolID = int.TryParse(ID, out int int_id);
                    bool boolSum = decimal.TryParse(txbx_money.Text, out decimal summ);
                    if (boolID && boolSum)
                    {
                        bank.Without(summ, int_id);
                    }
                    else
                        throw new Exception("Поля заполнены некорректно");
                    ViewDataGrid(dataGridAccounts, arrayList);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void btn_skipDay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (Account account in bank.accounts)
                {
                    account.Days++;

                    if (account.Days % 30 == 0)
                    {
                        bank.CalculatePercantage(account);
                    }

                }
                ViewDataGrid(dataGridAccounts, arrayList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = new FileStream("accounts.json", FileMode.OpenOrCreate))
            {
                //await JsonSerializer.SerializeAsync<List<Account>>(fs, bank.accounts);
                //MessageBox.Show("Объекты сериализованы");

            }
        }

        private async void desirialize_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = new FileStream("accounts.json", FileMode.OpenOrCreate))
            {
                //List<Account>? accounts = await JsonSerializer.DeserializeAsync<List<Account>>(fs);
                //foreach (Account account in accounts)
                //    arrayList.Add(account);
                //ViewDataGrid(dataGridAccounts, arrayList);



            }
        }

        private void txbx_money_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (txbx_money.Text != String.Empty)
                FlagTextBox = true;
            else if(txbx_money.Text=="")
                FlagTextBox = false;


        }

        private void radioDeposit_Checked(object sender, RoutedEventArgs e)
        {
            FlagRadio = true;
        }

        private void radioVostreb_Checked(object sender, RoutedEventArgs e)
        {
            FlagRadio = true;
        }

        private void radioDeposit_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void radioVostreb_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void dataGridAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = dataGridAccounts.SelectedItem;
            if(item!=null)
                FlagDataGrid = true;
            else
                FlagDataGrid = false;
        }
    }
}
