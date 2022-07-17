using System;
using System.Collections.Generic;
using System.Linq;
using CurrencyApp.Core;
using CurrencyApp.Model;
using CurrencyApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CurrencyApp.Repositories
{
	public class BankCurrenciesRepository: IBankCurrenciesRepository
	{
		public DBAppContext db { get; private set; }

		public BankCurrenciesRepository(DBAppContext db)
		{
			this.db = db;
		}

		//Cover with tests
		public List<BankCurrency> GetBankCurrenciesInCurrentDateRange(DateTime dateFrom, DateTime dateTo)
		{
			return db
				.BankCurrencies
				.Include(bc => bc.Currency)
				.Include(bc => bc.Bank)
				.Where(bc => bc.CreationDate <= dateTo && bc.CreationDate >= dateFrom).ToList();
		}
	}
}