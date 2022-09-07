using System.Collections.Generic;
using System.Linq;
using CurrencyApp.Core;
using CurrencyApp.Model;
using CurrencyApp.Repositories.Interfaces;

namespace CurrencyApp.Repositories
{
	public class CurrencyRepository: ICurrencyRepository
	{
		public DBAppContext db { get; private set; }

		public CurrencyRepository(DBAppContext db)
		{
			this.db = db;
		}

		public List<Currency> GetCurrencies()
		{
			return db.Currencies.ToList();
		}

		public Currency GetCurrencyById(int currencyId)
		{
			return db.Currencies.FirstOrDefault(c => c.Id == currencyId);
		}
	}
}