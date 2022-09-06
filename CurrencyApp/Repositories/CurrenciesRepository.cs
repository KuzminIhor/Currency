using System.Collections.Generic;
using System.Linq;
using CurrencyApp.Core;
using CurrencyApp.Model;
using CurrencyApp.Repositories.Interfaces;

namespace CurrencyApp.Repositories
{
	public class CurrenciesRepository: ICurrenciesRepository
	{
		public DBAppContext db { get; private set; }

		public CurrenciesRepository(DBAppContext db)
		{
			this.db = db;
		}

		public List<Currency> GetCurrencies()
		{
			return db.Currencies.ToList();
		}
	}
}