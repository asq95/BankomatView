using Bankomat.classes;
using MaterialDesignThemes.Wpf;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Bankomat.View
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private Account customer;
        private Bank bank = null;
        private int payoutValue = 0;
        public Menu(Bank bank, Account customer, int langOption)
        {
            InitializeComponent();
            while (!_contentLoaded)
            {
                Thread.Sleep(100); // blocks UI, but we don't care as it is ctor
            }
            this.bank = bank;
            this.customer = customer;
            LangBoxSwitch.SelectedIndex = langOption;
            SwitchLang(langOption);
        }

        private void SelectMenuButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
            {
                return;
            }

            if (button.Name == "Payout")
            {
                Wyplac.Visibility = Visibility.Visible;
                Saldo.Visibility = Visibility.Collapsed;
                Pin.Visibility = Visibility.Collapsed;
                if (Wyplac.Visibility == Visibility.Visible)
                {
                    PayoutBtn.Focus();
                }
                GridCursor.Margin = new Thickness(10, 0, 0, 0);
            }
            else if (button.Name == "Balance")
            {
                Wyplac.Visibility = Visibility.Collapsed;
                Saldo.Visibility = Visibility.Visible;
                Pin.Visibility = Visibility.Collapsed;
                GridCursor.Margin = new Thickness(210, 0, 0, 0);
            }
            else if (button.Name == "ChangePin")
            {
                Wyplac.Visibility = Visibility.Collapsed;
                Saldo.Visibility = Visibility.Collapsed;
                Pin.Visibility = Visibility.Visible;
                if (Pin.Visibility == Visibility.Visible)
                {
                    ChangePinBtn.Focus();
                }
                GridCursor.Margin = new Thickness(420, 0, 0, 0);
            }
        }

        private void SetChosenValue(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
            {
                return;
            }

            try
            {
                payoutValue = int.Parse(button.Uid);
                PayoutText.Text = payoutValue.ToString();
            }
            catch { }
        }

        private void PayoutClick(object sender, RoutedEventArgs e)
        {
            int value = payoutValue;
            if (value == 0)
            {
                return;
            }
            
            bank.Payout(customer, value);
            SaldoText.Text = customer.Balance.ToString();
        }

        private void ChangePinClick(object sender, RoutedEventArgs e)
        {
            if (CurrentPin.Password != customer.Pin || NewPin.Password != ConfirmNewPin.Password
               || NewPin.Password == String.Empty || ConfirmNewPin.Password == String.Empty || CurrentPin.Password == String.Empty)
            {
                MessageBox.Show("Current pin is wrong or new pins aren't the same, try again.", "Error");
                ResetPins();
                return;
            }

            bank.ChangePin(customer, NewPin.Password);
            ResetPins();
        }

        private void ResetPins()
        {
            CurrentPin.Password = String.Empty;
            ConfirmNewPin.Password = String.Empty;
            NewPin.Password = String.Empty;
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {

            MainWindow newMainWindow = new MainWindow(bank, LangBoxSwitch.SelectedIndex);
            newMainWindow.Show();
        
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

        private void DifferentValueChecker(object sender, TextChangedEventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box == null)
            {
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(box.Text, "^[0-9]*$") || box.Text.Length > 3)
            {
                box.Text = String.Empty;

            }

            try
            {
                int value = int.Parse(box.Text);
                if (value > 500)
                {
                    box.Text = String.Empty;
                }
            }
            catch {
                box.Text = String.Empty;
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
                WyplacPieniadzeLab.Text = "WYPŁAĆ PIENIĄDZE";
                PayoutTextConst.Text = "Ile chcesz wypłacić? Aktualnie chcesz wypłacić: ";
                DostepneSordkiText.Text = "DOSTĘPNE ŚRODKI";
                ZmienPinText.Text = "ZMIEŃ PIN";
                PayoutBtnTxt.Text = "Zatwierdź kwotę";
                DostepneBlock.Text = "Dostępne środki: ";
                HintAssist.SetHint(CurrentPin, "Aktualny PIN");
                HintAssist.SetHint(NewPin, "Nowy PIN");
                HintAssist.SetHint(ConfirmNewPin, "Powtórz nowy PIN");
                ZmienPinTxt.Text = "ZMIEŃ PIN";
            }
            else if (option == 1)
            {
                // EN
                WyplacPieniadzeLab.Text = "PAY OUT MONEY";
                PayoutTextConst.Text = "How much you want to pay out? Payout value: ";
                DostepneSordkiText.Text = "BALANCE";
                ZmienPinText.Text = "CHANGE PIN";
                PayoutBtnTxt.Text = "Confirm";
                DostepneBlock.Text = "Balance: ";
                HintAssist.SetHint(CurrentPin, "Current PIN");
                HintAssist.SetHint(NewPin, "New PIN");
                HintAssist.SetHint(ConfirmNewPin, "Repeat New PIN");
                ZmienPinTxt.Text = "CHANGE PIN";
            }
        }
    }
}
