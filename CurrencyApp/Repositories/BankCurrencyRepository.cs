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

		public List<BankCurrency> GetBankCurrenciesCollectionInDateRange(DateTime dateFrom, DateTime dateTo)
		{
			return db
				.BankCurrencies
				.Include(bc => bc.Currency)
				.Include(bc => bc.Bank)
				.Where(bc => bc.CreationDate <= dateTo && bc.CreationDate >= dateFrom).ToList();
		}

		public List<BankCurrency> GetBankCurrenciesCollection(int bankId)
		{
			return db.BankCurrencies
				.Include(bc => bc.Bank)
				.Include(bc => bc.Currency)
				.Where(bc => bc.Bank.Id == bankId).ToList();
		}

		public BankCurrency GetBankCurrency(int bankCurrencyId)
		{
			return db.BankCurrencies
				.Include(p => p.Currency)
				.Include(p => p.Bank)
				.FirstOrDefault(p => p.Id == bankCurrencyId);
		}

		public void AddBankCurrencyForm(Currency currency, Bank bank, double uahConvertation)
		{
			db.BankCurrencies.Add(new BankCurrency()
			{
				Currency = currency,
				Bank = bank,
				UAHConvertation = uahConvertation
			});

			db.SaveChanges();
		}

		public void UpdateBankCurrency(BankCurrency bankCurrency)
		{
			db.BankCurrencies.Update(bankCurrency);
			db.SaveChanges();
		}

		public void RemoveBankCurrency(BankCurrency bankCurrency)
		{
			db.BankCurrencies.Remove(bankCurrency);
			db.SaveChanges();
		}
	}
}