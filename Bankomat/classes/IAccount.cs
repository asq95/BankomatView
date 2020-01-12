namespace Bankomat.classes
{
    public interface IAccount
    {
        /// <summary>
        /// Klasa przechowująca dane klientów, takie jak saldo
        /// </summary>

        /// <summary>
        /// Zmienna przechowująca aktualne saldo
        /// Typ int
        /// </summary>
        int Balance { get; set; }

        /// <summary>
        /// Zmienna przechowująca aktualny PIN
        /// Typ string
        /// </summary>
        string Pin { get; set; }
    }
}