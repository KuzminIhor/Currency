﻿using System;
using System.Collections.Generic;
using CurrencyApp.Model;

namespace CurrencyApp.Repositories.Interfaces
{
	public interface IBankCurrencyRepository
	{
		public List<BankCurrency> GetBankCurrenciesCollectionInDateRange(DateTime dateFrom, DateTime dateTo);
		public List<BankCurrency> GetBankCurrenciesCollection(int bankId);
		public List<BankCurrency> GetBankCurrenciesCollectionByCurrency(int currencyId);
		public BankCurrency GetBankCurrency(int bankCurrencyId);
		public void AddBankCurrencyForm(Currency currency, Bank bank, double uahConvertation);
		public void UpdateBankCurrency(BankCurrency bankCurrency);
		public void RemoveBankCurrency(BankCurrency bankCurrency);
		public void RemoveBankCurrencies(List<BankCurrency> bankCurrencies);
	}
}