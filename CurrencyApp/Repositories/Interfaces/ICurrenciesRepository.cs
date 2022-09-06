using System.Collections.Generic;
using CurrencyApp.Model;

namespace CurrencyApp.Repositories.Interfaces
{
	public interface ICurrenciesRepository
	{
		public List<Currency> GetCurrencies();
	}
}