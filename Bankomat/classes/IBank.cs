namespace Bankomat.classes
{
    public interface IBank
    {
        /// <summary>
        /// Główna klasa sterująca całym bankomatem
        /// </summary>

        /// <summary>
        /// Funkcja, która pozwala na zmianie PINu osoby zalogowanej do banku
        /// </summary>
        void ChangePin(Account customer, string newPin);

        /// <summary>
        /// Funkcja używana do logowania sie do systemu bankomatu
        /// Zwraca obiekt klasy Account
        /// </summary>
        Account Login(string pin);

        /// <summary>
        /// Funkcja, która pozwala wypłacić pieniądze z dostępnego salda
        /// </summary>
        void Payout(Account customer, int value);
    }
}