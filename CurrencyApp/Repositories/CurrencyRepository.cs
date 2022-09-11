using System.Collections.Generic;
using System.Linq;
using CurrencyApp.Core;
using CurrencyApp.Model;
using CurrencyApp.Repositories.Interfaces;

namespace CurrencyApp.Repositories
{
	public class CurrencyRepository: ICurrencyRepository
	{
		public readonly DBAppContext db;

		public CurrencyRepository(DBAppContext db)
		{
			this.db = db;
		}

		public List<Currency> GetCurrencies()
		{
			return db.Currencies.ToList();
		}

		public Currency GetCurrency(int currencyId)
		{
			return db.Currencies.FirstOrDefault(c => c.Id == currencyId);
		}

		public void AddCurrency(Currency currency)
		{
			db.Currencies.Add(currency);
			db.SaveChanges();
		}

		public void UpdateCurrency(Currency currency)
		{
			db.Currencies.Update(currency);
			db.SaveChanges();
		}

		public void RemoveCurrency(Currency currency)
		{
			db.Currencies.Remove(currency);
			db.SaveChanges();
		}
	}
}