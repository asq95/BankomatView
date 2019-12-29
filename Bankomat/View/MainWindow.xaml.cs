using Bankomat.classes;
using System;
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
            bank = new Bank();
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

            Menu menu = new Menu(bank, customer);
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
    }
}
