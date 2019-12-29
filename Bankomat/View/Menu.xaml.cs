using Bankomat.classes;
using System;
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
        public Menu(Bank bank, Account customer)
        {
            InitializeComponent();
            this.bank = bank;
            this.customer = customer;
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

            payoutValue = int.Parse(button.Uid);
        }

        private void PayoutClick(object sender, RoutedEventArgs e)
        {
            int value = payoutValue;
            if (value == 0 && CustomValue.Text == String.Empty)
            {
                MessageBox.Show("Invalid payout value.", "Error");
                return;
            }

            if (value == 0)
            {
                value = int.Parse(CustomValue.Text);
            }

            bank.Payout(customer, value);
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

            int value = int.Parse(box.Text);
            if (value > 500)
            {
                box.Text = String.Empty;
            }
        }
    }
}
