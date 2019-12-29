namespace Bankomat.classes
{
    public class Account
    {
        private double balance;
        private string pin;

        public double Balance { get => balance; set => balance = value; }
        public string Pin { get => pin; set => pin = value; }

        public Account(string pin, double balance)
        {
            Balance = balance;
            Pin = pin;
        }
    }
}
