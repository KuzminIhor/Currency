using System.Collections.Generic;
using CurrencyApp.Model;

namespace CurrencyApp.Repositories.Interfaces
{
	public interface ICurrencyRepository
	{
		public List<Currency> GetCurrencies();
		public Currency GetCurrencyById(int currencyId);
	}
}