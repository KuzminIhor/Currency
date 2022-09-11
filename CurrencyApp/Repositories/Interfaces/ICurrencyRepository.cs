using System.Collections.Generic;
using CurrencyApp.Model;

namespace CurrencyApp.Repositories.Interfaces
{
	public interface ICurrencyRepository
	{
		public List<Currency> GetCurrencies();
		public Currency GetCurrency(int currencyId);
		public void AddCurrency(Currency currency);
		public void UpdateCurrency(Currency currency);
		public void RemoveCurrency(Currency currency);
	}
}