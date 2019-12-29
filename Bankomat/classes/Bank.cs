using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Bankomat.classes
{
    public class Bank
    {
        // accounts
        private List<Account> accounts;

        public Bank()
        {
            // deserialize saved accounts
            accounts = new List<Account>();
            DesrializeAccounts();
        }

        ~Bank()
        {
            SerializeAccounts();
        }

        public Account Login(string pin)
        {
            return accounts.Find(a => a.Pin == pin);
        }

        private void SerializeAccounts()
        {
            // save all accounts to json
            string json = JsonConvert.SerializeObject(accounts, Formatting.Indented);
            try
            {
                File.WriteAllText("data.json", json);
            }
            catch
            {
                MessageBox.Show("Failed to write data.", "Error");
            }
        }

        private void DesrializeAccounts()
        {
            // deserialize json file
            try
            {
                string data = File.ReadAllText("data.json");
                var obj = JsonConvert.DeserializeObject<List<Account>>(data);
                if (obj != null)
                {
                    accounts = obj;
                }
            }
            catch
            {
                MessageBox.Show("Error", "Failed to read data.");
            }
        }

        public void ChangePin(Account customer, string newPin)
        {
            customer.Pin = newPin;
            MessageBox.Show("Pin was succesfully changed.", "Success");
        }

        public void Payout(Account customer, int value)
        {
            if (customer.Balance - value < 0)
            {
                MessageBox.Show("You don't have enough money on your bank account.", "Error");
                return;
            }

            customer.Balance -= value;
            MessageBox.Show("Succesfully paid out " + value + " PLN.", "Success");
        }
    }
}
