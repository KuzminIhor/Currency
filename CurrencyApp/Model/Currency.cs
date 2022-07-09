using System.Collections.Generic;

namespace CurrencyApp.Model
{
    public class Currency
    {
		public int Id { get; set; }
        public string CurrencyName { get; set; }
        public List<BankCurrency> BankCurrencies { get; set; } = new List<BankCurrency>();
    }
}
