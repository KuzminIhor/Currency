using System;
using System.Collections.Generic;
using System.Linq;
using CurrencyApp.Core;
using CurrencyApp.Model;
using CurrencyApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CurrencyApp.Repositories
{
	public class BankCurrencyRepository: IBankCurrencyRepository
	{
		public readonly DBAppContext db;

		public BankCurrencyRepository(DBAppContext db)
		{
			this.db = db;
		}

		public List<BankCurrency> GetBankCurrenciesInCurrentDateRange(DateTime dateFrom, DateTime dateTo)
		{
			return db
				.BankCurrencies
				.Include(bc => bc.Currency)
				.Include(bc => bc.Bank)
				.Where(bc => bc.CreationDate <= dateTo && bc.CreationDate >= dateFrom).ToList();
		}

		public void AddBankCurrency(Currency currency, Bank bank, double uahConvertation)
		{
			db.BankCurrencies.Add(new BankCurrency()
			{
				Currency = currency,
				Bank = bank,
				UAHConvertation = uahConvertation
			});

			db.SaveChanges();
		}
	}
}