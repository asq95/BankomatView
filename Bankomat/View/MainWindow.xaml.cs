using Bankomat.classes;
using MaterialDesignThemes.Wpf;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Bankomat.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Bank bank = null;

        public MainWindow()
        {
            InitializeComponent();
            this.bank = new Bank();
        }

        public MainWindow(Bank bank, int langOption)
        {
            InitializeComponent();
            while (!_contentLoaded)
            {
                Thread.Sleep(100); // blocks UI, but we don't care as it is ctor
            }

            if (bank != null)
            {
                this.bank = bank;
            }
            else
            {
                this.bank = new Bank();
            }
            SwitchLang(langOption);
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            // login
            Account customer = bank.Login(PinEntryBox.Password);
            if (customer == null)
            {
                MessageBox.Show("Wrong pin", "Error");
                return;
            }

            Menu menu = new Menu(bank, customer, LangSwitchBox.SelectedIndex);
            menu.Show();
            this.Close();
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PinUpdate(object sender, RoutedEventArgs e)
        {
            // check validity
            PasswordBox box = sender as PasswordBox;
            if (box == null)
            {
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(box.Password, "^[0-9]*$") || box.Password.Length > 4)
            {
                box.Password = String.Empty;
            }
        }

        private void LanaguageSwitch(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox == null || !IsLoaded)
            {
                return;
            }

            SwitchLang(comboBox.SelectedIndex);
        }

        private void SwitchLang(int option)
        {
            if (option == 0)
            {
                // PL
                Header.Text = "Witamy w bankomacie";
                HintAssist.SetHint(PinEntryBox, "Wprowadź PIN");
                ZalogujButtonLabel.Text = "Zaloguj";
                WyjdzText.Text = "Wyjdź";
            }
            else if (option == 1)
            {
                // EN
                Header.Text = "Welcome to your ATM";
                HintAssist.SetHint(PinEntryBox, "Input PIN");
                ZalogujButtonLabel.Text = "Login";
                WyjdzText.Text = "Exit";
            }
        }
    }
}
