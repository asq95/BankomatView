namespace Bankomat.classes
{
    public class Account : IAccount
    {
        private int balance;
        private string pin;

        public int Balance { get => balance; set => balance = value; }
        public string Pin { get => pin; set => pin = value; }

        public Account(string pin, int balance)
        {
            Balance = balance;
            Pin = pin;
        }
    }
}
