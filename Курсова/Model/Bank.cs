using System.Collections.Generic;

namespace CurrencyApp.Model
{
    public class Bank
    {
        public int Id { get; set; }
        public string BankName { get; set; }
        public List<BankCurrency> BankCurrencies { get; set; } = new List<BankCurrency>();
    }
}
